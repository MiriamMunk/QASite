using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QASite.Data
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string HashPassword { get; set; }
        public List<Likes> Likes { get; set; }
        public List<LikedAnswers> LikedAnswers { get; set; }
        public List<Question> Questions { get; set; }
        public List<Answers> Answers { get; set; }
    }
}
