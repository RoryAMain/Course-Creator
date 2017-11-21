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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebDevProject.Controllers
{
    public class HomeController : Controller
    {
        private ModelContext _context;
        private IConfigurationRoot _config;

        public HomeController(ModelContext context, IConfigurationRoot config)
        {
            _context = context;
            _config = config;
        }

        // Index Actions
        [HttpGet]
        public IActionResult Index()
        {
            var moduleInfo = from mod in _context.Module
                              select mod;

            IndexViewModel model = new IndexViewModel();

            Index index = _context.Index.SingleOrDefault();

            model.theIndex = index;

            if(moduleInfo != null)
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

            if(referenceInfo != null)
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
            if(ModelState.IsValid)
            {
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
        public ActionResult IndexAddReference(string Link,string Text,int Id)
        {
            if (ModelState.IsValid)
            {
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
                Index theIndex = _context.Index.SingleOrDefault(ind => ind.Id == Id);
                theIndex.youtubeURL = Link;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost ("IndexEditLecture")]
        public ActionResult IndexEditLecturePost(string lectureText, int Id)
        {
            if (ModelState.IsValid)
            {
                Index theIndex = _context.Index.SingleOrDefault(ind => ind.Id == Id);
                theIndex.lectureText = lectureText;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost ("IndexUploadFile")]
        public async Task<IActionResult> Post(int Id, IFormFile file)
        {
            long size = file.Length;
            var filePath = ("wwwroot/mp4/" + file.FileName);
            string fileName = file.FileName;
            
            if(file.Length > 0)
            {
                using (var stream = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return RedirectToAction("IndexEditMP4", new { Link = fileName,Id = Id});

        }

        [HttpGet]
        public ActionResult IndexEditMP4(string Link, int Id)
        {
            if (ModelState.IsValid)
            {
                Index theIndex = _context.Index.SingleOrDefault(ind => ind.Id == Id);
                theIndex.MP4Link = Link;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        //Module Actions

        [HttpGet]
        public IActionResult ModuleView(int Id)
        {
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

            return View(model);
        }

        [HttpPost("ModuleEditLecture")]
        public ActionResult ModuleEditLecturePost(string lectureText, int Id)
        {
            if (ModelState.IsValid)
            {
                Module theModule = _context.Module.SingleOrDefault(mod => mod.Id == Id);
                theModule.lectureText = lectureText;
                _context.SaveChanges();
                return RedirectToAction("ModuleView",new { Id = Id});
            }

            return View();
        }

        [HttpPost]
        public ActionResult ModuleAddTopic(string topicTitle, int Id)
        {
            if (ModelState.IsValid)
            {
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
                Module theModule = _context.Module.SingleOrDefault(mod => mod.Id == Id);
                theModule.videoURL = Link;
                _context.SaveChanges();
                return RedirectToAction("ModuleView", new {Id= Id});
            }

            return View();
        }

        [HttpPost("ModuleUploadFile")]
        public async Task<IActionResult> ModulePost(int Id, IFormFile file)
        {
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

        [HttpGet]
        public ActionResult ModuleEditMP4(string Link, int Id)
        {
            if (ModelState.IsValid)
            {
                Module theModule = _context.Module.SingleOrDefault(mod => mod.Id == Id);
                theModule.MP4Link = Link;
                _context.SaveChanges();
                return RedirectToAction("ModuleView",new { Id = Id});
            }

            return View();
        }


        //Topic Actions

        [HttpGet]
        public IActionResult TopicView(int Id)
        {
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

            return View(model);
        }

        [HttpPost("TopicEditLecture")]
        public ActionResult TopicEditLecturePost(string lectureText, int Id)
        {
            if (ModelState.IsValid)
            {
                Topic theTopic = _context.Topic.SingleOrDefault(top => top.Id == Id);
                theTopic.lectureText = lectureText;
                _context.SaveChanges();
                return RedirectToAction("TopicView", new { Id = Id });
            }

            return View();
        }

        [HttpPost]
        public ActionResult TopicAddQuestion(string QuestionType, string QuestionString, int Id)
        {
            if (ModelState.IsValid)
            {
                var questionInfo = from q in _context.Question
                                   where q.TopicId == Id
                                   select q;
                int questionOrder = questionInfo.Count();



                if (QuestionType == "Coding")
                {
                    Question question = new Question();
                    question.TopicId = Id;
                    question.isMultipleChoice = false;
                    question.questionString = QuestionString;
                    question.questionOrder = questionOrder;
                    _context.Question.Add(question);
                    _context.SaveChanges();
                    return RedirectToAction("TopicView", new { Id = Id });
                }

                else
                {
                    Question question = new Question();
                    question.TopicId = Id;
                    question.isMultipleChoice = true;
                    question.questionString = QuestionString;
                    question.questionOrder = questionOrder;
                    _context.Question.Add(question);
                    _context.SaveChanges();
                    return RedirectToAction("TopicView", new { Id = Id });
                }

            }

            return View();
        }

        [HttpPost]
        public ActionResult TopicAddReference(string Link, string Text, int Id)
        {
            if (ModelState.IsValid)
            {
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
                Topic theTopic = _context.Topic.SingleOrDefault(top => top.Id == Id);
                theTopic.videoURL = Link;
                _context.SaveChanges();
                return RedirectToAction("TopicView", new { Id = Id });
            }

            return View();
        }

        [HttpPost("TopicUploadFile")]
        public async Task<IActionResult> TopicPost(int Id, IFormFile file)
        {
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

        [HttpGet]
        public ActionResult TopicEditMP4(string Link, int Id)
        {
            if (ModelState.IsValid)
            {
                Topic theTopic = _context.Topic.SingleOrDefault(top => top.Id == Id);
                theTopic.MP4Link = Link;
                _context.SaveChanges();
                return RedirectToAction("TopicView", new { Id = Id});
            }

            return View();
        }

        //Multiple Choice Actions

        public IActionResult MultipleChoiceView(int Id, int topicId)
        {
            MultipleChoiceViewModel model = new MultipleChoiceViewModel();

            Question question = _context.Question.SingleOrDefault(quest => quest.Id == Id);

            model.theQuestion = question;

            var questionInfo = from q in _context.Question
                               where q.TopicId == topicId
                               select q;

            var orderListLength = questionInfo.Count();
            model.orderListLength = orderListLength-1;
            
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
            CodeQuestionViewModel model = new CodeQuestionViewModel();

            Question question = _context.Question.SingleOrDefault(quest => quest.Id == Id);

            model.theQuestion = question;

            var questionInfo = from q in _context.Question
                               where q.TopicId == topicId
                               select q;

            var orderListLength = questionInfo.Count();
            model.orderListLength = orderListLength-1;

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
        public ActionResult QuestionAddReference(string Link, string Text, int TopicId,string isMultipleChoice, int Id)
        {
            if (ModelState.IsValid)
            {
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
    }
}
