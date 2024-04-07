using Project.Domain.Entities;


namespace Project.Application.DTOs
{
    public class ChapterDTO
    {
        public Guid Id { get; set; }
        public string title { get; set; }
        public List<Topic>? tipicList { get; set; }
        public List<Questions>? QuestionsList { get; set; }
    }
}
