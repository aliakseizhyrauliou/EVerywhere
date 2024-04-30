namespace EVerywhere.ModulesCommon.Application.Exceptions;

public class ForbiddenAccessException(string message) : Exception(message);