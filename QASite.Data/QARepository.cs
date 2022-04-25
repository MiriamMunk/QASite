using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QASite.Data
{
    public class QARepository
    {
        private readonly string _ConnectionString;
        public QARepository(string connString)
        {
            _ConnectionString = connString;
        }
        public List<Question> GetQuestions()
        {
            using var context = new QADataContext(_ConnectionString);
            return context.Questions.Include(qt => qt.QuetionTags).ThenInclude(t => t.Tag).Include(l => l.Likes)
                .Include(a => a.Answers).OrderByDescending(d => d.DatePosted).ToList();
        }
        public Question GetQuestionById(int id)
        {
            using var context = new QADataContext(_ConnectionString);
            var q = context.Questions.Where(x => x.Id == id).Include(u => u.User).Include(qt => qt.QuetionTags)
                .ThenInclude(t => t.Tag).Include(l => l.Likes).FirstOrDefault();
            if (q != null)
            {
                q.Answers = new List<Answers>(GetAnswers(q.Id));
            }
            return q;
        }
        public List<Answers> GetAnswers(int id)
        {
            using var context = new QADataContext(_ConnectionString);
            return context.Answers.Include(u => u.User).Include(l => l.LikedAnswers).Where(q => q.QuestionId == id)
                .OrderByDescending(d => d.DateSubmitted).ToList();
        }
        public void AddUser(User u)
        {
            using var context = new QADataContext(_ConnectionString);
            context.User.Add(u);
            context.SaveChanges();
        }
        public User GetUserByEmail(string email)
        {
            using var context = new QADataContext(_ConnectionString);
            return context.User.FirstOrDefault(x => x.Email == email);
        }
        public User LogIn(string email, string password)
        {
            using var context = new QADataContext(_ConnectionString);
            var user = GetUserByEmail(email);
            if (user == null)
            {
                return null;
            }
            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.HashPassword);
            return isValid ? user : null;
        }
        public Tag GetTagByName(string name)
        {
            using var context = new QADataContext(_ConnectionString);
            return context.Tags.FirstOrDefault(t => t.Name == name);
        }
        public int AddTag(string tag)
        {
            using var context = new QADataContext(_ConnectionString);
            Tag t = new() { Name = tag };
            context.Tags.Add(t);
            context.SaveChanges();
            return t.Id;
        }
        public int AddQuestion(Question q, List<string> tags)
        {
            using var context = new QADataContext(_ConnectionString);
            context.Questions.Add(q);
            context.SaveChanges();

            foreach (string t in tags)
            {
                int TagId;
                var tag = GetTagByName(t);
                if (tag == null)
                {
                    TagId = AddTag(t);
                }
                else
                {
                    TagId = tag.Id;
                }
                context.QuestionsTags.Add(new QuestionTags { QuestionId = q.Id, TagID = TagId });
            }
            context.SaveChanges();
            return q.Id;
        }
        public void AddAnswer(Answers answer)
        {
            using var context = new QADataContext(_ConnectionString);
            context.Answers.Add(answer);
            context.SaveChanges();
        }
        public int GetQuestionLikes(int id)
        {
            using var context = new QADataContext(_ConnectionString);
            return context.Likes.Where(x => x.QuestionID == id).Count();
        }
        public void AddQuestionLike(int Qid, int Uid)
        {
            using var context = new QADataContext(_ConnectionString);
            context.Likes.Add(new Likes { QuestionID = Qid, UserId = Uid });
            context.SaveChanges();
        }
        public void AddAnswerLike(int Aid, int Uid)
        {
            using var context = new QADataContext(_ConnectionString);
            context.LikedAnswers.Add(new LikedAnswers { AnswerId = Aid, UserId = Uid });
            context.SaveChanges();
        }
        public List<Question> GetQuestionsFromTag(string name)
        {
            using var context = new QADataContext(_ConnectionString);
            return context.Questions.Include(qt => qt.QuetionTags).ThenInclude(t => t.Tag)
                .Where(x => x.QuetionTags.Any(t => t.Tag.Name == name)).Include(l => l.Likes)
                .Include(a => a.Answers).ToList();
        }
        public List<Answers> GetAnswersForQ(int id)
        {
            using var context = new QADataContext(_ConnectionString);
            return context.Answers.Where(x => x.QuestionId == id).Include(l => l.LikedAnswers).ToList();
        }
    }
}
