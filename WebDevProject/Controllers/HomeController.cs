using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebDevProject.Controllers;
using WebDevProject.Models;
//using WebDevProject.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System.IO;
using System.Text;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebDevProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ModelContext _context;
        private IConfigurationRoot _config;

        public HomeController(ModelContext context, IConfigurationRoot config, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _config = config;
            _userManager = userManager;
        }

        // Index Actions
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Email = _userManager.GetUserName(HttpContext.User);

            var moduleInfo = from mod in _context.Module
                             select mod;

            IndexViewModel model = new IndexViewModel();

            Index index = _context.Index.SingleOrDefault();

            model.theIndex = index;

            if (moduleInfo != null)
            {
                model.Modules = (from module in moduleInfo
                                 select new Module()
                                 {
                                     moduleTitle = module.moduleTitle,
                                     Id = module.Id
                                 }).ToList();
            }

            //Right now this assumes there will only be one Index. In the case that more Indexes are ever added(for more classes),
            //then modify this search with "where reference.indexId == Id", where Id is passed in to this IActionResult.
            var referenceInfo = from reference in _context.IndexReferenceList
                                select reference;

            if (referenceInfo != null)
            {
                model.referenceList = (from reference in referenceInfo
                                       select new IndexReferenceList()
                                       {
                                           Link = reference.Link,
                                           Text = reference.Text
                                       }).ToList();
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult IndexAddModule(string moduleTitle, int Id)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Email = _userManager.GetUserName(HttpContext.User);

                var moduleInfo = from mod in _context.Module
                                 select mod;
                int moduleOrder = moduleInfo.Count();

                Module module = new Module();
                module.moduleTitle = moduleTitle;
                module.moduleOrder = moduleOrder;
                _context.Module.Add(module);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public ActionResult IndexAddReference(string Link, string Text, int Id)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Email = _userManager.GetUserName(HttpContext.User);

                IndexReferenceList referenceList = new IndexReferenceList();
                referenceList.Link = Link;
                referenceList.Text = Text;
                referenceList.IndexId = Id;
                _context.IndexReferenceList.Add(referenceList);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public ActionResult IndexEditYoutube(string Link, int Id)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Email = _userManager.GetUserName(HttpContext.User);

                Index theIndex = _context.Index.SingleOrDefault(ind => ind.Id == Id);
                theIndex.youtubeURL = Link;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost("IndexEditLecture")]
        public ActionResult IndexEditLecturePost(string lectureText, int Id)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Email = _userManager.GetUserName(HttpContext.User);

                Index theIndex = _context.Index.SingleOrDefault(ind => ind.Id == Id);
                theIndex.lectureText = lectureText;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }


        [HttpGet]
        public ActionResult IndexEditMP4(string Link, int Id)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Email = _userManager.GetUserName(HttpContext.User);

                Index theIndex = _context.Index.SingleOrDefault(ind => ind.Id == Id);
                theIndex.MP4Link = Link;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost("IndexUploadMP4")]
        public async Task<IActionResult> IndexPostMP4(int Id, IFormFile file)
        {
            ViewBag.Email = _userManager.GetUserName(HttpContext.User);

            long size = file.Length;
            var filePath = ("wwwroot/mp4/" + file.FileName);
            string fileName = file.FileName;

            if (file.Length > 0)
            {
                using (var stream = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return RedirectToAction("IndexEditMP4", new { Link = fileName, Id = Id });

        }

        [HttpPost]
        public ActionResult IndexAddLink(string Link, string Text, int Id)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Email = _userManager.GetUserName(HttpContext.User);

                IndexReferenceList referenceList = new IndexReferenceList();
                referenceList.Link = Link;
                referenceList.Text = Text;
                referenceList.IndexId = Id;
                _context.IndexReferenceList.Add(referenceList);
                _context.SaveChanges();
                return RedirectToAction("Index", new { Id = Id });
            }

            return View();
        }

        [HttpGet]
        public ActionResult IndexAddFile(string Link, string Text, int Id)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Email = _userManager.GetUserName(HttpContext.User);

                IndexReferenceList referenceList = new IndexReferenceList();
                referenceList.Link = Link;
                referenceList.Text = Text;
                referenceList.IndexId = Id;
                _context.IndexReferenceList.Add(referenceList);
                _context.SaveChanges();
                return RedirectToAction("Index", new { Id = Id });
            }

            return View();
        }

        [HttpPost("IndexUploadFile")]
        public async Task<IActionResult> IndexPostFile(int Id, IFormFile file)
        {
            ViewBag.Email = _userManager.GetUserName(HttpContext.User);

            long size = file.Length;
            var filePath = ("wwwroot/upload/" + file.FileName);
            string fileName = file.FileName;

            if (file.Length > 0)
            {
                using (var stream = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            var fileLink = "../../upload/" + file.FileName;

            return RedirectToAction("IndexAddFile", new { Link = fileLink, Text = fileName, Id = Id });

        }

        //Module Actions

        [HttpGet]
        public IActionResult ModuleView(int Id)
        {
            ViewBag.Email = _userManager.GetUserName(HttpContext.User);

            var topicInfo = from top in _context.Topic
                            where top.ModuleId == Id
                            select top;

            ModuleViewModel model = new ModuleViewModel();

            Module module = _context.Module.SingleOrDefault(mod => mod.Id == Id);

            if (topicInfo != null)
            {
                model.Topics = (from top in topicInfo
                                select new Topic()
                                {
                                    topicTitle = top.topicTitle,
                                    Id = top.Id
                                }).ToList();
            }

            model.theModule = module;

            var referenceInfo = from reference in _context.ModuleReferenceList
                                where reference.ModuleId == Id
                                select reference;

            if (referenceInfo != null)
            {
                model.referenceList = (from reference in referenceInfo
                                       select new ModuleReferenceList()
                                       {
                                           Link = reference.Link,
                                           Text = reference.Text
                                       }).ToList();
            }

            var moduleInfo = from mod in _context.Module
                             select mod;

            var orderListLength = moduleInfo.Count();
            model.orderListLength = orderListLength - 1;

            if (moduleInfo != null)
            {
                model.moduleList = (from mod in moduleInfo
                                    select new Module()
                                    {
                                        Id = mod.Id,
                                        moduleOrder = mod.moduleOrder
                                    }).ToList();
            }

            return View(model);
        }

        [HttpPost("ModuleEditLecture")]
        public ActionResult ModuleEditLecturePost(string lectureText, int Id)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Email = _userManager.GetUserName(HttpContext.User);

                Module theModule = _context.Module.SingleOrDefault(mod => mod.Id == Id);
                theModule.lectureText = lectureText;
                _context.SaveChanges();
                return RedirectToAction("ModuleView", new { Id = Id });
            }

            return View();
        }

        [HttpPost]
        public ActionResult ModuleAddTopic(string topicTitle, int Id)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Email = _userManager.GetUserName(HttpContext.User);

                var topicInfo = from top in _context.Topic
                                where top.ModuleId == Id
                                select top;
                int topicOrder = topicInfo.Count();

                Topic topic = new Topic();
                topic.topicTitle = topicTitle;
                topic.ModuleId = Id;
                topic.topicOrder = topicOrder;
                _context.Topic.Add(topic);
                _context.SaveChanges();
                return RedirectToAction("ModuleView", new { Id = Id });
            }

            return View();
        }

        [HttpPost]
        public ActionResult ModuleAddReference(string Link, string Text, int Id)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Email = _userManager.GetUserName(HttpContext.User);

                ModuleReferenceList referenceList = new ModuleReferenceList();
                referenceList.Link = Link;
                referenceList.Text = Text;
                referenceList.ModuleId = Id;
                _context.ModuleReferenceList.Add(referenceList);
                _context.SaveChanges();
                return RedirectToAction("ModuleView", new { Id = Id });
            }

            return View();
        }

        [HttpPost]
        public ActionResult ModuleEditYoutube(string Link, int Id)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Email = _userManager.GetUserName(HttpContext.User);

                Module theModule = _context.Module.SingleOrDefault(mod => mod.Id == Id);
                theModule.videoURL = Link;
                _context.SaveChanges();
                return RedirectToAction("ModuleView", new { Id = Id });
            }

            return View();
        }

        [HttpGet]
        public ActionResult ModuleEditMP4(string Link, int Id)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Email = _userManager.GetUserName(HttpContext.User);

                Module theModule = _context.Module.SingleOrDefault(mod => mod.Id == Id);
                theModule.MP4Link = Link;
                _context.SaveChanges();
                return RedirectToAction("ModuleView", new { Id = Id });
            }

            return View();
        }

        [HttpPost("ModuleUploadMP4")]
        public async Task<IActionResult> ModulePostMP4(int Id, IFormFile file)
        {
            ViewBag.Email = _userManager.GetUserName(HttpContext.User);

            long size = file.Length;
            var filePath = ("wwwroot/mp4/" + file.FileName);
            string fileName = file.FileName;

            if (file.Length > 0)
            {
                using (var stream = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return RedirectToAction("ModuleEditMP4", new { Link = fileName, Id = Id });

        }

        [HttpPost]
        public ActionResult ModuleAddLink(string Link, string Text, int Id)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Email = _userManager.GetUserName(HttpContext.User);

                ModuleReferenceList referenceList = new ModuleReferenceList();
                referenceList.Link = Link;
                referenceList.Text = Text;
                referenceList.ModuleId = Id;
                _context.ModuleReferenceList.Add(referenceList);
                _context.SaveChanges();
                return RedirectToAction("ModuleView", new { Id = Id });
            }

            return View();
        }

        [HttpGet]
        public ActionResult ModuleAddFile(string Link, string Text, int Id)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Email = _userManager.GetUserName(HttpContext.User);

                ModuleReferenceList referenceList = new ModuleReferenceList();
                referenceList.Link = Link;
                referenceList.Text = Text;
                referenceList.ModuleId = Id;
                _context.ModuleReferenceList.Add(referenceList);
                _context.SaveChanges();
                return RedirectToAction("ModuleView", new { Id = Id });
            }

            return View();
        }

        [HttpPost("ModuleUploadFile")]
        public async Task<IActionResult> ModulePostFile(int Id, IFormFile file)
        {
            ViewBag.Email = _userManager.GetUserName(HttpContext.User);

            long size = file.Length;
            var filePath = ("wwwroot/upload/" + file.FileName);
            string fileName = file.FileName;

            if (file.Length > 0)
            {
                using (var stream = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            var fileLink = "upload/" + file.FileName;

            return RedirectToAction("ModuleAddFile", new { Link = fileLink, Text = fileName, Id = Id });

        }


        //Topic Actions

        [HttpGet]
        public IActionResult TopicView(int Id)
        {
            ViewBag.Email = _userManager.GetUserName(HttpContext.User);

            var questionInfo = from q in _context.Question
                               where q.TopicId == Id
                               select q;

            TopicViewModel model = new TopicViewModel();

            Topic topic = _context.Topic.SingleOrDefault(top => top.Id == Id);

            if (questionInfo != null)
            {
                model.Questions = (from q in questionInfo
                                   select new Question()
                                   {
                                       Id = q.Id,
                                       isMultipleChoice = q.isMultipleChoice,
                                       questionOrder = q.questionOrder
                                   }).ToList();
            }

            model.theTopic = topic;

            var referenceInfo = from reference in _context.TopicReferenceList
                                where reference.TopicId == Id
                                select reference;

            if (referenceInfo != null)
            {
                model.referenceList = (from reference in referenceInfo
                                       select new TopicReferenceList()
                                       {
                                           Link = reference.Link,
                                           Text = reference.Text
                                       }).ToList();
            }

            var moduleId = from top in _context.Topic
                           where top.Id == Id
                           select top.ModuleId;

            var topicInfo = from top in _context.Topic
                            where top.ModuleId == moduleId.SingleOrDefault()
                            select top;

            var orderListLength = questionInfo.Count();
            model.orderListLength = orderListLength - 1;

            if (topicInfo != null)
            {
                model.topicList = (from top in topicInfo
                                   select new Topic()
                                   {
                                       topicOrder = top.topicOrder
                                   }).ToList();
            }

            return View(model);
        }

        [HttpPost("TopicEditLecture")]
        public ActionResult TopicEditLecturePost(string lectureText, int Id)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Email = _userManager.GetUserName(HttpContext.User);

                Topic theTopic = _context.Topic.SingleOrDefault(top => top.Id == Id);
                theTopic.lectureText = lectureText;
                _context.SaveChanges();
                return RedirectToAction("TopicView", new { Id = Id });
            }

            return View();
        }

        [HttpPost]
        public ActionResult TopicAddCodingQuestion(string questionString, string answerString, string codeString, int Id)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Email = _userManager.GetUserName(HttpContext.User);

                var questionInfo = from q in _context.Question
                                   where q.TopicId == Id
                                   select q;
                int questionOrder = questionInfo.Count();

                Question question = new Question();
                question.TopicId = Id;
                question.isMultipleChoice = false;
                question.questionString = questionString;
                question.correctCodeAnswer = answerString;
                question.suppliedCode = codeString;
                question.questionOrder = questionOrder;
                _context.Question.Add(question);
                _context.SaveChanges();
                return RedirectToAction("TopicView", new { Id = Id });


            }

            return View();
        }

        [HttpPost]
        public ActionResult TopicAddMultipleChoiceQuestion(string questionString, string choiceString1, string choiceString2, string choiceString3, string choiceString4, string correctAnswer, int Id)
        {
            if (ModelState.IsValid)
            {
                int correctAnswerAsInt = 0;
                if (Int32.TryParse(correctAnswer, out correctAnswerAsInt))
                {

                    ViewBag.Email = _userManager.GetUserName(HttpContext.User);

                    var questionInfo = from q in _context.Question
                                       where q.TopicId == Id
                                       select q;
                    int questionOrder = questionInfo.Count();

                    Question question = new Question();
                    question.TopicId = Id;
                    question.isMultipleChoice = true;
                    question.questionString = questionString;
                    question.questionOrder = questionOrder;
                    question.multipleChoice1 = choiceString1;
                    question.multipleChoice2 = choiceString2;
                    question.multipleChoice3 = choiceString3;
                    question.multipleChoice4 = choiceString4;
                    question.correctMultipleChoice = correctAnswerAsInt;
                    _context.Question.Add(question);
                    _context.SaveChanges();
                    return RedirectToAction("TopicView", new { Id = Id });
                }

            }

            return RedirectToAction("TopicView", new { Id = Id });
        }

        [HttpPost]
        public ActionResult TopicAddLink(string Link, string Text, int Id)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Email = _userManager.GetUserName(HttpContext.User);

                TopicReferenceList referenceList = new TopicReferenceList();
                referenceList.Link = Link;
                referenceList.Text = Text;
                referenceList.TopicId = Id;
                _context.TopicReferenceList.Add(referenceList);
                _context.SaveChanges();
                return RedirectToAction("TopicView", new { Id = Id });
            }

            return View();
        }

        [HttpGet]
        public ActionResult TopicAddFile(string Link, string Text, int Id)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Email = _userManager.GetUserName(HttpContext.User);

                TopicReferenceList referenceList = new TopicReferenceList();
                referenceList.Link = Link;
                referenceList.Text = Text;
                referenceList.TopicId = Id;
                _context.TopicReferenceList.Add(referenceList);
                _context.SaveChanges();
                return RedirectToAction("TopicView", new { Id = Id });
            }

            return View();
        }

        [HttpPost]
        public ActionResult TopicEditYoutube(string Link, int Id)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Email = _userManager.GetUserName(HttpContext.User);

                Topic theTopic = _context.Topic.SingleOrDefault(top => top.Id == Id);
                theTopic.videoURL = Link;
                _context.SaveChanges();
                return RedirectToAction("TopicView", new { Id = Id });
            }

            return View();
        }

        [HttpPost("TopicUploadMP4")]
        public async Task<IActionResult> TopicPostMP4(int Id, IFormFile file)
        {
            ViewBag.Email = _userManager.GetUserName(HttpContext.User);

            long size = file.Length;
            var filePath = ("wwwroot/mp4/" + file.FileName);
            string fileName = file.FileName;

            if (file.Length > 0)
            {
                using (var stream = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return RedirectToAction("TopicEditMP4", new { Link = fileName, Id = Id });

        }

        [HttpPost("TopicUploadFile")]
        public async Task<IActionResult> TopicPostFile(int Id, IFormFile file)
        {
            ViewBag.Email = _userManager.GetUserName(HttpContext.User);

            long size = file.Length;
            var filePath = ("wwwroot/upload/" + file.FileName);
            string fileName = file.FileName;

            if (file.Length > 0)
            {
                using (var stream = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            var fileLink = "upload/" + file.FileName;

            return RedirectToAction("TopicAddFile", new { Link = fileLink, Text = fileName, Id = Id });

        }

        [HttpGet]
        public ActionResult TopicEditMP4(string Link, int Id)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Email = _userManager.GetUserName(HttpContext.User);

                Topic theTopic = _context.Topic.SingleOrDefault(top => top.Id == Id);
                theTopic.MP4Link = Link;
                _context.SaveChanges();
                return RedirectToAction("TopicView", new { Id = Id });
            }

            return View();
        }

        //Multiple Choice Actions

        public IActionResult MultipleChoiceView(int Id, int topicId)
        {
            ViewBag.Email = _userManager.GetUserName(HttpContext.User);

            MultipleChoiceViewModel model = new MultipleChoiceViewModel();

            Question question = _context.Question.SingleOrDefault(quest => quest.Id == Id);

            model.theQuestion = question;

            var questionInfo = from q in _context.Question
                               where q.TopicId == topicId
                               select q;

            var orderListLength = questionInfo.Count();
            model.orderListLength = orderListLength - 1;

            if (questionInfo != null)
            {
                model.questionList = (from q in questionInfo
                                      select new Question()
                                      {
                                          Id = q.Id,
                                          isMultipleChoice = q.isMultipleChoice,
                                          TopicId = q.TopicId,
                                          questionOrder = q.questionOrder
                                      }).ToList();
            }

            var referenceInfo = from reference in _context.QuestionReferenceList
                                where reference.QuestionId == Id
                                select reference;

            if (referenceInfo != null)
            {
                model.referenceList = (from reference in referenceInfo
                                       select new QuestionReferenceList()
                                       {
                                           Link = reference.Link,
                                           Text = reference.Text
                                       }).ToList();
            }

            return View(model);
        }



        //Code Question Actions

        public IActionResult CodeQuestionView(int Id, int topicId)
        {
            ViewBag.Email = _userManager.GetUserName(HttpContext.User);

            CodeQuestionViewModel model = new CodeQuestionViewModel();

            Question question = _context.Question.SingleOrDefault(quest => quest.Id == Id);

            model.theQuestion = question;

            var questionInfo = from q in _context.Question
                               where q.TopicId == topicId
                               select q;

            var orderListLength = questionInfo.Count();
            model.orderListLength = orderListLength - 1;

            if (questionInfo != null)
            {
                model.questionList = (from q in questionInfo
                                      select new Question()
                                      {
                                          Id = q.Id,
                                          isMultipleChoice = q.isMultipleChoice,
                                          TopicId = q.TopicId,
                                          questionOrder = q.questionOrder
                                      }).ToList();
            }

            var referenceInfo = from reference in _context.QuestionReferenceList
                                where reference.QuestionId == Id
                                select reference;

            if (referenceInfo != null)
            {
                model.referenceList = (from reference in referenceInfo
                                       select new QuestionReferenceList()
                                       {
                                           Link = reference.Link,
                                           Text = reference.Text
                                       }).ToList();
            }

            return View(model);
        }


        [HttpPost]
        public ActionResult QuestionAddReference(string Link, string Text, int TopicId, string isMultipleChoice, int Id)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Email = _userManager.GetUserName(HttpContext.User);

                QuestionReferenceList referenceList = new QuestionReferenceList();
                referenceList.Link = Link;
                referenceList.Text = Text;
                referenceList.QuestionId = Id;
                _context.QuestionReferenceList.Add(referenceList);
                _context.SaveChanges();
            }

            if (isMultipleChoice == "True")
            {
                return RedirectToAction("MultipleChoiceView", new { Id = Id, TopicId = TopicId });
            }
            else
            {
                return RedirectToAction("CodeQuestionView", new { Id = Id, TopicId = TopicId });
            }
        }

        [HttpPost("QuestionEditCode")]
        public ActionResult QuestionEditCodePost(string writtenCode, int Id, int topicId)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Email = _userManager.GetUserName(HttpContext.User);

                Question theQuestion = _context.Question.SingleOrDefault(q => q.Id == Id);
                theQuestion.suppliedCode = writtenCode;
                _context.SaveChanges();

                string path = "wwwroot/pythonScripts/studentScript.py";
                System.IO.File.WriteAllText(path, writtenCode);

                return RedirectToAction("CodeQuestionView", new { Id = Id, TopicId = topicId });
            }

            return View();
        }

        [HttpPost]
        public ActionResult QuestionRunPython(int Id, int TopicId)
        {

            string cmd = "wwwroot/pythonScripts/studentScript.py";
            string args = "";

            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "C:/Python27/python.exe";
            start.Arguments = string.Format("\"{0}\" \"{1}\"", cmd, args);
            start.UseShellExecute = false;// Do not use OS shell
            start.CreateNoWindow = true; // We don't need new window
            start.RedirectStandardOutput = true;// Any output, generated by application will be redirected back
            start.RedirectStandardError = true; // Any error in standard output will be redirected back (for example exceptions)
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string stderr = process.StandardError.ReadToEnd(); // Here are the exceptions from our Python script
                    string result = reader.ReadToEnd(); // Here is the result of StdOut(for example: print "test")
                    Debug.WriteLine("STDERR: " + stderr);
                    Debug.WriteLine("STDOUT: " + result);
                    if(ModelState.IsValid)
                    {
                        Question question = _context.Question.SingleOrDefault(quest => quest.Id == Id);
                        if(stderr=="" || stderr ==null)
                        {
                            question.compiledResult = result;
                        }
                        else
                        {
                            question.compiledResult = stderr;
                        }

                        _context.SaveChanges();
                    }

                    return RedirectToAction("CodeQuestionView", new { Id = Id, TopicId = TopicId });
                }

            }
        }
    }
}
