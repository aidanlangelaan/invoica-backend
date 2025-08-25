namespace Invoica.Domain.Exceptions;

public class MissingClaimException(string message) : Exception(message);