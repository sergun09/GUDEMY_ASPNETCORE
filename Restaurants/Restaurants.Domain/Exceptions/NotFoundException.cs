namespace Restaurants.Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string ressourceType, string ressourceIdentifier) : 
        base($"{ressourceType} with id: {ressourceIdentifier} does not exist")
    {
    }
}
