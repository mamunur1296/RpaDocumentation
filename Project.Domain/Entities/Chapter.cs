using Project.Domain.Entities.Base;

namespace Project.Domain.Entities
{
    public class Chapter : BaseEntity
    {
        public string  title {  get; set; }
        public List<Topic> ? tipicList { get; set; }
        public List<Questions> ? QuestionsList { get; set; }
    }
}
