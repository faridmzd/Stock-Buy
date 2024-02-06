using Microsoft.IdentityModel.Tokens;

namespace Stock_Buy.API.Domain
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
    }
}
