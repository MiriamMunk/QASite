using QASite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QASite.Web.Models
{
    public class ViewModel
    {
        public string Message { get; set; }
        public List<Question> Questions { get; set; }
        public Question Question { get; set; }
        public bool LikedQuestion { get; set; }
        public List<int> LikedAnswer { get; set; }
        public string TagName { get; set; }
    }
}
