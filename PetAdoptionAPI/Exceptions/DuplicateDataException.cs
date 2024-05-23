namespace PetAdoptionAPI.Exceptions;

public class DuplicateDataException : Exception
{
    public DuplicateDataException()
    {
    }

    public DuplicateDataException(string? message) : base(message)
    {
    }
}