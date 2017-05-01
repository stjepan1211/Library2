﻿using System.Web;
using System.Web.Optimization;

namespace Library.WebApi
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/app/js").Include(
                   "~/Scripts/angular.js",
                   "~/Scripts/angular-ui-router.js",
                   "~/app/js/ngStorage.js",
                   "~/app/js/localStorage.js",
                   "~/app/js/dirPagination.js"

               ));

            bundles.Add(new ScriptBundle("~/app").Include(
                    "~/app/app.js",
                    "~/app/controllers/*Controller.js",
                    "~/app/services/*Service.js"
                ));
        }
    }
}
