/*
 * Entity<T> class represents an abstract base class for entities in the domain.
 * It provides common properties such as Id, CreatedAt, CreatedBy, LastModified, and LastModifiedBy.
 */

namespace Permissions.Domain.Abstractions
{
    public abstract class Entity<T> : IEntity<T>
    {
        public T Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
