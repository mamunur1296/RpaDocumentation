namespace Project.Presentation.Models
{
    public class QuestionsModel
    {
        public Guid Id { get; set; }
        public string title { get; set; }
        public string answers { get; set; }
        public Guid TopicId { get; set; }
    }
}
