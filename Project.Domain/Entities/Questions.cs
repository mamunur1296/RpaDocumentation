using Project.Domain.Entities.Base;


namespace Project.Domain.Entities
{
    public class Questions : BaseEntity
    {
        public string title { get; set; }
        public string answers { get; set; }
        public Guid  TopicId { get; set; }
        public Guid chapterId { get; set; }
    }
}
