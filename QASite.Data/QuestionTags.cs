using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QASite.Data
{
    public class QuestionTags
    {
        public int QuestionId { get; set; }
        public int TagID { get; set; }
        public Question Question { get; set; }
        public Tag Tag { get; set; }
    }
}
