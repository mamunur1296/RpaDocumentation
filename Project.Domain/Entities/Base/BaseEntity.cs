using System.ComponentModel.DataAnnotations;

namespace Project.Domain.Entities.Base
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; private set; }
        public BaseEntity() 
        {
            ModifiedDate = DateTime.Now;
        }
    }
}
