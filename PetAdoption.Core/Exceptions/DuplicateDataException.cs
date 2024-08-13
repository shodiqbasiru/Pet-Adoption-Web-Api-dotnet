namespace PetAdoption.Core.Exceptions;

public class DuplicateDataException : Exception
{
    public DuplicateDataException()
    {
    }

    public DuplicateDataException(string? message) : base(message)
    {
    }
}