using Project.Presentation.Models;

namespace Project.Presentation.ViewModel
{
    public class ChapterViewModel
    {
        public ChapterModel Chapter { get; set; }
        public List<ChapterModel> ChapterList { get; set; } = new List<ChapterModel>();
        public List<TopicModel> TopicList { get; set; } = new List<TopicModel>();
        public List<QuestionsModel> QuestionsList { get; set; } = new List<QuestionsModel>();
    }
}
