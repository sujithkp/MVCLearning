using System.Web;
using System.Web.Optimization;

namespace MVCLearning
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.7.2.min.js",
                        "~/Scripts/jquery-ui-1.8.18.min.js",
                        "~/Scripts/fb-metro-ui-autocomplete.js",
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.unobtrusive.min.js"
                        ));


            bundles.Add(new ScriptBundle("~/bundles/customdataannotations").Include(
                        "~/Scripts/Validations/notRequiredWhen.js",
                        "~/Scripts/Validations/requiredWhen.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css", "~/Content/ExampleSite.css", "~/Content/jquery.ui.autocomplete.css", "~/Content/jquery.ui.all.css", "~/Content/jquery.ui.theme.css",
                      "~/Content/site.css"));
        }
    }
}
