namespace Project.Application.DTOs
{
    public class TopicDTO
    {
        public Guid Id { get; set; }
        public string title { get; set; }
        public Guid Chapterid { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? Created_By { get; set; }
        public string? Modified_By { get; set; }

    }
}
