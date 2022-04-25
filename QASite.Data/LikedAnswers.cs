using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QASite.Data
{
    public class LikedAnswers
    {
        public int UserId { get; set; }
        public int AnswerId { get; set; }
        public User User { get; set; }
        public Answers Answer { get; set; }
    }
}
