using System.Web;
using System.Web.Optimization;

namespace UTestProject
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //StyleBundles
            bundles.Add(new StyleBundle("~/css/common.css")
                .Include(
                      "~/Static/css/bootstrap.css",
                      "~/Static/css/all.min.css",
                      "~/Static/css/fontawesome.min.css",
                      "~/Static/css/style.css"
                      ));
            bundles.Add(new StyleBundle("~/css/allExam.css")
                .Include("~/Static/css/allExam.css"));

            bundles.Add(new StyleBundle("~/css/apexcharts.css")
                .Include("~/Static/css/apexcharts.css"));

            bundles.Add(new StyleBundle("~/css/dashboard.css")
                .Include("~/Static/css/dashboard.css"));

            bundles.Add(new StyleBundle("~/css/exam.css")
                .Include("~/Static/css/exam.css"));

            bundles.Add(new StyleBundle("~/css/footer.css")
                .Include("~/Static/css/footer.css"));

            bundles.Add(new StyleBundle("~/css/leaderboard.css")
                .Include("~/Static/css/leaderboard.css"));

            bundles.Add(new StyleBundle("~/css/liveExlam.css")
                .Include("~/Static/css/liveExlam.css"));

            bundles.Add(new StyleBundle("~/css/liveQuiz.css")
                .Include("~/Static/css/liveQuiz.css"));

            bundles.Add(new StyleBundle("~/css/login.css")
                .Include("~/Static/css/login.css"));

            bundles.Add(new StyleBundle("~/css/mock.css")
                .Include("~/Static/css/mock.css"));

            bundles.Add(new StyleBundle("~/css/performance.css")
                .Include("~/Static/css/performance.css"));

            bundles.Add(new StyleBundle("~/css/profile.css")
                .Include("~/Static/css/profile.css"));

            bundles.Add(new StyleBundle("~/css/review.css")
                .Include("~/Static/css/review.css"));

            bundles.Add(new StyleBundle("~/css/setting.css")
                .Include("~/Static/css/setting.css"));

            bundles.Add(new StyleBundle("~/css/userProfile.css")
                .Include("~/Static/css/userProfile.css"));


            //ScriptBundle

            bundles.Add(new ScriptBundle("~/js/bootstrap.js")
                .Include("~/Static/js/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/js/bootstrap.js")
                .Include("~/Static/js/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/js/apexchart.js")
                .Include("~/Static/js/apexchart.js"));

            bundles.Add(new ScriptBundle("~/js/exam.js")
                .Include("~/Static/js/exam.js"));

            bundles.Add(new ScriptBundle("~/js/question.js")
                .Include("~/Static/js/question.js"));

            bundles.Add(new ScriptBundle("~/js/script.js")
                .Include("~/Static/js/script.js"));

            bundles.Add(new ScriptBundle("~/js/userProfile.js")
                .Include("~/Static/js/userProfile.js"));

        }
    }
}
