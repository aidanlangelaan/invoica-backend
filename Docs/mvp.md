# Invoica - MVP

## Scope
- Collect work per customer in an **Order** (one or many gigs/days).
- Generate an **Invoice** from an Order.
- Reuse Company/Customer defaults (day rate, km rate, payment term).
- Fast line entry (Day/KM/Hour/Fixed) with ad-hoc overrides.
- UBL invoice generation.

> Currently out of scope:
> - Payment tracking
> - Fixed asset management (materials)

## Entities

### Company (Seller)
- Id (PK)
- Name
- Email
- Phone
- AddressId (FK → Address)
- VatNumber
- ChamberOfCommerceNumber
- Iban
- Bic

### Customer (Buyer)
- Id (PK)
- DisplayName
- ContactName (optional)
- Email
- Phone
- BillingAddressId (FK → Address)
- VatNumber (optional)
- PaymentTermDays (optional; overrides company default)
- DayRate (optional; overrides company default)
- KmRate (optional; overrides company default)

### Address
- Id (PK)
- AddressLine1 
- AddressLine2 (optional)
- AddressLine3 (optional)
- PostalCode (optional)
- Locality
- AdministrativeArea (optional)
- CountryCode (ISO 3166-1 alpha-2)

### Order
- Id (PK)
- CompanyId (FK)
- CustomerId (FK)
- OrderDate
- Description (free text; e.g., “All July shows” or a specific gig)
- Notes (optional)
- Status (`Draft`, `Ready`, `Invoiced`)

### OrderLine
- Id (PK)
- OrderId (FK)
- Description (free text)
- Quantity (decimal)
- UnitCode (`DAY`, `HUR`, `KMT`, `C62`) // UNECE Rec 20
- UnitPrice (decimal(18,2))
- VatRateId (FK → VatRates)

### Invoice
- Id (PK)
- InvoiceNumber (string; unique; user-editable while `Draft`)
- IssueDate
- DueDate
- CurrencyCode (ISO 4217; default `EUR`)
- CompanyId (FK)
- CustomerId (FK)
- OrderId (FK)
- Subject (free text)
- BillingAddressSnapshot (embedded copy at issue time)
- SubtotalExclVat (decimal(18,2))
- TotalVat (decimal(18,2))
- TotalInclVat (decimal(18,2))
- Status (`Draft`, `Issued`)

### InvoiceLine
- Id (PK)
- InvoiceId (FK)
- Description
- Quantity
- UnitCode
- UnitPrice
- VatRate (e.g., 21.00, 9.00, 0.00)
- VatCategoryCode (e.g., S, AA, Z, E, AE)
- VatExemptionReasonCode (optional)
- VatExemptionReason (optional) // human-readable reason if required
- LineTotalExclVat
- LineVatAmount
- LineTotalInclVat

### Document (PDF of XML)
- Id (PK)
- InvoiceId (FK → Invoice)
- FileName
- ContentType // e.g., application/pdf, application/xml
- StoragePathOrBlob
- CreatedAt
- 
### VatRate (catalog)
- Id (PK)
- CountryCode (ISO 3166-1 alpha-2, e.g., `NL`)
- Percentage (21.00, 9.00, 0.00)
- CategoryCode (UNCL5305 subset: S, AA, Z, E, AE, ...)
- ExemptionReasonCode (optional)   // e.g., VATEX-EU-AE (reverse charge)
- DisplayName (string)                     // e.g., "VAT NL Standard (21%)"
- IsActive (bool)
- SortOrder (int)

## API (MVP)

### Orders
- GET /orders
- POST /orders
- GET /orders/{id}
- PUT /orders/{id}
- POST /orders/{id}/lines
- PUT /orders/{id}/lines/{lineId}
- DELETE /orders/{id}/lines/{lineId}
- POST /orders/{id}/generate-invoice   → returns Invoice (Draft with copied lines)

### Invoices
- GET /invoices
- GET /invoices/{id}
- PUT /invoices/{id}                    → editable only while `Draft` (incl. InvoiceNumber and Subject)
- POST /invoices/{id}/issue             → validate + lock number + snapshot + totals
- POST /invoices/{id}/pdf               → store document 
- POST /invoices/{id}/xml               → store document
- GET /invoices/{id}/download           → returns stored PDF

### Company / Customers / VatRates
- As defined above (including defaults on Company/Customer).

### VAT Rates
- GET /vatrates
- POST /vatrates
- PUT /vatrates/{id}
- DELETE /vatrates/{id}

## Computation & Validation Rules

### Line calculations
- LineTotalExclVat = round(Quantity * UnitPrice, 2)
- LineVatAmount    = round(LineTotalExclVat * (VatRate / 100), 2)
- LineTotalInclVat = LineTotalExclVat + LineVatAmount

### Invoice totals
- SubtotalExclVat = sum(LineTotalExclVat)
- TotalVat        = sum(LineVatAmount)
- TotalInclVat    = SubtotalExclVat + TotalVat

### General validation
- At least one line required.
- Quantity ≥ 0, UnitPrice ≥ 0.
- VatRate ∈ {21.00, 9.00, 0.00} for NL MVP.
- DueDate ≥ IssueDate.
- On `issue`: snapshot address + VAT percentages; freeze lines & totals.

## Invoice Numbering

**Approach:** fetch previous number and increment; if a user enters an already-used number, return a clear error.

### Recommended algorithm (safe, simple)
1. When creating a Draft:
   - Suggest `proposedNumber` = `MAX(existing) + 1` (or a formatted variant).
   - Do not persist/lock it yet.
2. On `POST /invoices/{id}/issue` (single DB transaction):
   - Use the user-provided `InvoiceNumber` if present; otherwise use the `proposedNumber`.
   - Enforce a **UNIQUE index** on `InvoiceNumber`.
   - On unique-violation, return **409 Conflict** with a helpful message and a fresh suggestion.

### Implementation notes
- Create unique index: `UX_Invoices_InvoiceNumber` on `InvoiceNumber`.
- Wrap `issue` in a transaction; catch unique-violation → 409.
- If you later add year-prefixes or multi-user concurrency, you can introduce a `NumberingSequence` without breaking this model.

## PDF Footer (auto-generated)
- The footer is rendered during PDF generation from the effective context (Company IBAN/BIC, DueDate from payment terms, etc.), not stored on `Invoice`.