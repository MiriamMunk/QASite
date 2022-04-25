using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QASite.Data
{
    public class Answers
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public string Name { get; set; }
        public DateTime DateSubmitted { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<LikedAnswers> LikedAnswers { get; set; }
    }
}
