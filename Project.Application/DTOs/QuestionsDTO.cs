using Project.Domain.Entities;


namespace Project.Application.DTOs
{
    public class QuestionsDTO
    {
        public Guid Id { get; set; }
        public string title { get; set; }
        public string answers { get; set; }
        public Guid TopicId { get; set; }

    }
}
