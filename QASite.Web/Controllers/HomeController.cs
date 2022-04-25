using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QASite.Data;
using QASite.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QASite.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _connectionString;
        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        public IActionResult Home()
        {
            var repo = new QARepository(_connectionString);
            return View(new ViewModel { Questions = repo.GetQuestions() });
        }
        [Authorize]
        public IActionResult AskQuestion()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddQuestion(string title, string text, List<string> tags)
        {
            QARepository repo = new(_connectionString);
            int QId = repo.AddQuestion(new Question
            {
                Title = title,
                Text = text,
                DatePosted = DateTime.Now,
                UserId = repo.GetUserByEmail(User.Identity.Name).Id
            }, tags);
            return Redirect($"/home/viewQuestion?id={QId}");
        }
        public IActionResult ViewQuestion(int id)
        {
            QARepository repo = new(_connectionString);
            ViewModel vm = new();
            var q = repo.GetQuestionById(id);
            if (q == null)
            {
                return Redirect("/home/home");
            }
            vm.Question = q;
            var LikedQuestions = HttpContext.Session.Get<List<int>>("QuestionIds") ?? new List<int>();
            if (LikedQuestions.Contains(id))
            {
                vm.LikedQuestion = true;
            }
            var LikedAnswers = HttpContext.Session.Get<List<int>>("AnswerIds") ?? new List<int>();
            vm.LikedAnswer = new(LikedAnswers);

            return View(vm);
        }
        public IActionResult Addanswer(int questionid, string answer)
        {
            QARepository repo = new(_connectionString);
            repo.AddAnswer(new Answers
            {
                Name = repo.GetUserByEmail(User.Identity.Name).Name,
                Answer = answer,
                QuestionId = questionid,
                DateSubmitted = DateTime.Now,
                UserId = repo.GetUserByEmail(User.Identity.Name).Id
            });
            return Redirect($"/home/ViewQuestion?id={questionid}");
        }
        public IActionResult GetTag(string name)
        {
            QARepository repo = new(_connectionString);
            if (repo.GetTagByName(name) == null)
            {
                return Redirect("/home/home");
            }
            return View(new ViewModel { Questions = repo.GetQuestionsFromTag(name), TagName = name });
        }
    }
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static T Get<T>(this ISession session, string key)
        {
            string value = session.GetString(key);

            return value == null ? default :
                JsonConvert.DeserializeObject<T>(value);
        }
    }
}
