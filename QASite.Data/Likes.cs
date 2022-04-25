using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QASite.Data
{
    public class Likes
    {
        public int UserId { get; set; }
        public int QuestionID { get; set; }
        public User User { get; set; }
        public Question Question { get; set; }
    }
}
