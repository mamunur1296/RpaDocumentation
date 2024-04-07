namespace Project.Presentation.Models
{
    public class TopicModel
    {
        public Guid Id { get; set; }
        public string title { get; set; }
        public Guid Chapterid { get; set; }
        public List<QuestionsModel>? QuestionsList { get; set; }
    }
}
