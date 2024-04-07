using Project.Domain.Entities;

namespace Project.Presentation.Models
{
    public class ChapterModel
    {
        public Guid Id { get; set; }
        public string title { get; set; }
        public List<TopicModel>? tipicList { get; set; }
        public List<QuestionsModel>? QuestionsList { get; set; }
    }
}
