using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTestProject.Models;

namespace UTestProject.Controllers
{
    public class ExamController : Controller
    {

        SubjectDbEntities subjectDb = new SubjectDbEntities();
        StudentDbEntities studentDb = new StudentDbEntities();
        QuestionDbEntities questionDb = new QuestionDbEntities();
        ExamDbEntities examDb = new ExamDbEntities();

        [HttpGet]
        public ActionResult Mock()
        {
            var email = Session["UserEmail"];
            var pass = Session["UserPass"];
            if (email != null && pass != null)
            {
                Student student = studentDb.Students.Where(temp => temp.Email == email && temp.Password == pass).FirstOrDefault();
                List<Subject> subjects = subjectDb.Subjects.Where(temp => temp.Category == student.Category).ToList();

                ViewBag.subjects = subjects;
                ViewBag.category = student.Category;
                return View();

            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }

        [ActionName("Mock")]
        [HttpPost]
        public ActionResult MockForm()
        {
            List<Subject> subjectList = subjectDb.Subjects.ToList();
            List<int?> selectedSubjects = new List<int?>();
            string selectedSubjectsStr = "";
            string questionIdListStr = "";


            string subjectInput = "", numOfQuestionsInput = "", negMarkingInput = "";
            int numOfQuestions = 0, negMarking = 0, examDuration = 0, category = 0;

            category = Int32.Parse(Request.Form["category"]);
            numOfQuestionsInput = Request.Form["numOfQuestions"];
            negMarkingInput = Request.Form["negMarking"];
            examDuration = Int32.Parse(Request.Form["examDuration"]);

            numOfQuestions = String.IsNullOrWhiteSpace(numOfQuestionsInput) ? 0 : Int32.Parse(numOfQuestionsInput);
            negMarking = Int32.Parse(negMarkingInput);


            for (int i = 0; i < subjectList.Count; i++)
            {
                Subject subject = subjectList[i];
                subjectInput = Request.Form["subject_" + subject.ID];

                if (!String.IsNullOrWhiteSpace(subjectInput))
                {
                    selectedSubjects.Add(Int32.Parse(subjectInput));
                    selectedSubjectsStr += subject.Title + ",";
                }

            }

            int totalQuestions = questionDb.Questions.Count();

            Random rand = new Random();

            List<Question> questionList = questionDb.Questions
            .Where(temp => selectedSubjects.Contains(temp.Subject))
            .OrderBy(temp => Guid.NewGuid()) // For random selection from DB
            .Take(numOfQuestions) // Limit list of record
            .ToList();


            foreach (var item in questionList)
            {
                questionIdListStr += item.ID + ",";
            }

            ViewBag.questionList = questionList;
            ViewBag.numOfQuestions = numOfQuestions;
            ViewBag.selectedSubjectsStr = selectedSubjectsStr;
            ViewBag.negMarking = negMarking;
            ViewBag.examDuration = examDuration;
            ViewBag.category = category;
            ViewBag.questionIdListStr = questionIdListStr;

            return View("MockExam");

        }

        //[HttpGet]
        //public ActionResult MockExam()
        //{
        //    List<Subject> subjectlist = subjectDb.Subjects.ToList();
        //    List<Question> questionList = questionDb.Questions.ToList();
        //    ViewBag.subjectlist = subjectlist;
        //    ViewBag.questionList = questionList;
        //    return View();
        //}

        [ActionName("MockExam")]
        [HttpPost]
        public ActionResult MockExamForm()
        {
            var email = Session["UserEmail"];
            var pass = Session["UserPass"];
            if (email != null && pass != null)
            {
                Student student = studentDb.Students.Where(temp => temp.Email == email && temp.Password == pass).FirstOrDefault();
                List<int> questionIdList = new List<int>();
                string questionIdListStr = Request.Form["questionIdListStr"];
                string selectedSubjectsStr = Request.Form["selectedSubjectsStr"];
                string examDuration = Request.Form["examDuration"];
                string negMarking = Request.Form["negMarking"];


                questionIdList = listStrToListOfInt(questionIdListStr);

                List<Question> questionList = questionDb.Questions
                .Where(temp => questionIdList.Contains(temp.ID))
                .ToList();

                int totalCorrectAns = 0, totalWrongAns = 0, totalSkippedAns = 0;
                double obtainedMarks = 0, totalMarks = 0;
                int totalQuestions = questionIdList.Count();

                List<string> correctAnswers = new List<string>();
                List<string> markedAnswers = new List<string>();
                List<int> answerStatus = new List<int>();
                foreach (Question question in questionList)
                {
                    correctAnswers.Add(question.Correct_ans);
                    string optionValueInput = Request.Form["optionRadioBtn_" + question.ID];
                    if (!String.IsNullOrEmpty(optionValueInput))
                    {
                        if (question.Correct_ans == optionValueInput)
                        {
                            totalCorrectAns++;
                            answerStatus.Add(1);
                            markedAnswers.Add(question.Correct_ans);
                        }
                        else
                        {
                            totalWrongAns++;
                            answerStatus.Add(-1);
                            markedAnswers.Add(optionValueInput);
                        }
                    }
                    else
                    {
                        totalSkippedAns++;
                        answerStatus.Add(0);
                        markedAnswers.Add("skipped");
                    }
                }

                totalMarks = questionList.Count();
                if (negMarking == "0")
                {
                    obtainedMarks = totalCorrectAns;
                }
                else
                {
                    obtainedMarks = totalCorrectAns - totalWrongAns * 0.25;
                    obtainedMarks = (obtainedMarks < 0) ? 0 : obtainedMarks;
                }

                DateTime examDate = DateTime.Now;
                Exam newExam = new Exam()
                {
                    Category = student.Category,
                    Student = student.ID,
                    Subjects = selectedSubjectsStr,
                    Obtained_marks = obtainedMarks,
                    Total_marks = totalMarks,
                    Exam_date = examDate,
                    Total_correct_ans = totalCorrectAns,
                    Total_wrong_ans = totalWrongAns,
                    Total_skipped_ans = totalSkippedAns
                };
                examDb.Exams.Add(newExam);
                examDb.SaveChanges();

                ViewBag.questionList = questionList;
                ViewBag.selectedSubjectsStr = selectedSubjectsStr;
                ViewBag.examDuration = examDuration;
                ViewBag.obtainedMarks = obtainedMarks;
                ViewBag.totalCorrectAns = totalCorrectAns;
                ViewBag.totalWrongAns = totalWrongAns;
                ViewBag.totalSkippedAns = totalSkippedAns;
                ViewBag.correctAnswers = correctAnswers;
                ViewBag.answerStatus = answerStatus;
                ViewBag.markedAnswers = markedAnswers;

                return View("ExamResult");


            }
            else
            {
                return RedirectToAction("Login", "User");
            }


        }


        // Converts list string(format->"Bangla,English,Math,") to array of string(format->["Bangla","English","Math"])
        public string[] listStrToListOfStr(string listStr)
        {
            List<int> list = new List<int>();
            char[] spearator = { ',' };
            String[] strlist = listStr.Split(spearator);
            return strlist;
        }

        // Converts list string(format->"1,2,8,") to list of int(format->[1,2,8])
        public List<int> listStrToListOfInt(string listStr)
        {
            List<int> list = new List<int>();

            char[] spearator = { ',' };

            String[] strlist = listStr.Split(spearator);
            foreach (var item in strlist)
            {
                if (!String.IsNullOrWhiteSpace(item))
                    list.Add(Int32.Parse(item));
            }
            return list;

        }

        //public ActionResult ExamResult()
        //{

        //    return View();
        //}
        public ActionResult AllExam()
        {

            var email = Session["UserEmail"];
            var pass = Session["UserPass"];
            if (email != null && pass != null)
            {
                Student student = studentDb.Students.Where(temp => temp.Email == email && temp.Password == pass).FirstOrDefault();

                List<Exam> exams = examDb.Exams
                .Where(exam => exam.Student == student.ID)
                .ToList();

                ViewBag.exams = exams;
                return View();

            }
            else
            {
                return RedirectToAction("Login", "User");
            }


            return View();
        }
    }
}