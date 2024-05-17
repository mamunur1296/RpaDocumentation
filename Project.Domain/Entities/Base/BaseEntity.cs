using System.ComponentModel.DataAnnotations;

namespace Project.Domain.Entities.Base
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get;  set; }
        public string? Created_By { get; set; }
        public string? Modified_By { get; set; }
        
    }
}
