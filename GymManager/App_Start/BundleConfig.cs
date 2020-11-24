using System.Web;
using System.Web.Optimization;

namespace GymManager
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/umd/popper.js",
                        "~/Scripts/bootstrap.js",
                        "~/scripts/datatables/jquery.datatables.js",
                        "~/scripts/datatables/datatables.bootstrap4.js",
                        "~/scripts/bootbox.js",
                        "~/scripts/toastr.js",
                        "~/scripts/GymManager/toastrOptions.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/GymManager/gm-custom-validate-rules.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-pulse.css",
                      "~/Content/fontawesome-all.css",
                      "~/Content/datatables/css/dataTables.bootstrap4.min.css",
                      "~/Content/DataTables/css/buttons.bootstrap4.css",
                      "~/content/toastr.css",
                      "~/Content/site.css"));
        }
    }
}
