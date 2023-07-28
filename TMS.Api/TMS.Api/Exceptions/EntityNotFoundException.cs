namespace TMS.Api.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() { }
        
        public EntityNotFoundException(string errorMessage) : base(errorMessage) { }
        
        public EntityNotFoundException(string errorMessage, Exception innerException) : base(errorMessage, innerException) { }

        public EntityNotFoundException(int entityId, string entityName) : base(FormattableString.Invariant($"'{entityName}' with id '{entityId}' was not found")) { }
    }
}
