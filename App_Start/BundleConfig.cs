using System.Web;
using System.Web.Optimization;

namespace ProjectBasedSystem
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/angularjs").Include(
                "~/Scripts/angular.min.js",
                "~/Scripts/ui-bootstrap-tpls-2.5.0.js",
                "~/Scripts/angular-route.min.js",
                "~/Scripts/angular-ui-router.js",
                "~/Scripts/app/app.js",
                "~/Scripts/app/routes.js",
                "~/Scripts/app/Controllers/DataList.js",
               "~/Scripts/app/Controllers/modal.js",
                "~/Scripts/app/Services/Resident.Services.js",
                "~/Scripts/app/Services/Nationality.Services.js",
                "~/Scripts/app/Services/Barangay.Services.js",
                "~/Scripts/app/Services/Religion.Services.js",
                "~/Scripts/app/Services/Resident.Directives.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/site.css",
                      "~/Content/font-awesome.min.css"));
        }
    }
}
