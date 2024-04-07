using Project.Domain.Entities.Base;


namespace Project.Domain.Entities
{
    public class Topic : BaseEntity
    {
        public string title { get; set; }
        public Guid Chapterid { get; set; }
        public List<Questions> QuestionsList { get; set; } = new List<Questions>();
    }
}
