using Project.Domain.Entities;


namespace Project.Application.DTOs
{
    public class QuestionsDTO
    {
        public Guid Id { get; set; }
        public string title { get; set; }
        public string answers { get; set; }
        public Guid TopicId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? Created_By { get; set; }
        public string? Modified_By { get; set; }

    }
}
