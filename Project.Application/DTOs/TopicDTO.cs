using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs
{
    public class TopicDTO
    {
        public Guid Id { get; set; }
        public string title { get; set; }
        public Guid Chapterid { get; set; }
        public List<Questions> ? QuestionsList { get; set; }

    }
}
