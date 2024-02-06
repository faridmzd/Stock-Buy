namespace Stock_Buy.API.ExceptionHandling
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {

        }

        public static void ThrowIfNull(object? entity, Guid id, string entityName)
        {
            if (entity is null) throw new NotFoundException(message: $"{entityName} with given Id \'{id}\' does not exist!");
        }

        public static void ThrowIfFalse(bool entityExists, Guid id, string entityName)
        {
            if (!entityExists) throw new NotFoundException(message: $"{entityName} with given Id \'{id}\' does not exist!");
        }


    }

    public class DuplicateEntityException : Exception
    {
        public DuplicateEntityException(string message) : base(message)
        {

        }
        public static void ThrowIfTrue(bool entityExists, string duplicateValueName, string entityName)
        {
            if (entityExists) throw new DuplicateEntityException(message: $"{entityName} with given \'{duplicateValueName}\' already exists!");
        }

    }


}
