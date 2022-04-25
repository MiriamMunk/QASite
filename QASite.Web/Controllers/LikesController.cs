using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QASite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QASite.Web.Controllers
{
    public class LikesController : Controller
    {
        private readonly string _connectionString;
        public LikesController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        public IActionResult GetQuestionlikes(int id)
        {
            QARepository repo = new(_connectionString);
            return Json(new { likes = repo.GetQuestionLikes(id) });
        }
        [HttpPost]
        public void AddQuestionLike(int QuestionID)
        {
            QARepository repo = new(_connectionString);
            repo.AddQuestionLike(QuestionID, repo.GetUserByEmail(User.Identity.Name).Id);

            var LikedQuestions = HttpContext.Session.Get<List<int>>("QuestionIds") ?? new List<int>();
            LikedQuestions.Add(QuestionID);
            HttpContext.Session.Set("QuestionIds", LikedQuestions);
        }
        [HttpPost]
        public void AddAnswerLike(int AnswerId)
        {
            QARepository repo = new(_connectionString);
            repo.AddAnswerLike(AnswerId, repo.GetUserByEmail(User.Identity.Name).Id);

            var LikedAnswers = HttpContext.Session.Get<List<int>>("AnswerIds") ?? new List<int>();
            LikedAnswers.Add(AnswerId);
            HttpContext.Session.Set("AnswerIds", LikedAnswers);
        }
    }
}
