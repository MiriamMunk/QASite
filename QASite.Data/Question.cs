﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QASite.Data
{
    public class Question
    {
        public int Id { get; set; }                 
        public string Title { get; set; }   
        public string Text { get; set; }
        public DateTime DatePosted { get; set;}
        public int UserId { get; set; }
        public List<QuestionTags> QuetionTags { get; set; }
        public List<Likes> Likes { get; set; }
        public User User { get; set; }
        public List<Answers> Answers { get; set; }
    }
}
