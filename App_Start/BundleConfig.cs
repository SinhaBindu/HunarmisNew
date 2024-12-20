﻿using System.Web;
using System.Web.Optimization;

namespace Hunarmis
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/mvcfoolproof.unobtrusive.min.js"));

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

            bundles.Add(new StyleBundle("~/Content/design").Include(
                      "~/Content/css/bootstrap.min.css",
                      "~/Content/css/design-css/animate.css",
                      "~/Content/css/design-css/login.css",
                      "~/Content/css/design-css/fonts.css",
                      //"~/Content/css/design-css/font-awesome.css",
                      "~/Content/css/design-css/glyphicon.css"
            ));

            bundles.Add(new StyleBundle("~/Content/AdminCSS").Include(
                "~/Content/css/bootstrap.min.css",
                "~/Content/css/nifty.min.css",
                "~/Content/css/bootstrap-switch.css",
                "~/Content/css/demo/nifty-demo-icons.min.css",
                "~/Content/plugins/pace/pace.min.css",
                "~/Content/css/style.css",
                "~/Content/css/responsive.css",
                "~/Content/css/main.css",
                //"~/Content/plugins/bootstrap-tagsinput/bootstrap-tagsinput.min.css",
                "~/Content/plugins/select2/css/select2.min.css",
                "~/Content/css/printdesign.css",
                "~/Content/css/AdminGlobal.css"
           ));

            bundles.Add(new ScriptBundle("~/bundles/AdminScript").Include(
                "~/Content/js/bootstrap.min.js",
                "~/Content/js/nifty.min.js",
                "~/Content/plugins/pace/pace.min.js",
                //"~/Content/plugins/bootstrap-tagsinput/bootstrap-tagsinput.min.js",
                "~/Content/plugins/select2/js/select2.min.js",
                "~/Scripts/jquery.unobtrusive-ajax.min.js",
                "~/Scripts/app.js"
            ));



        }
    }
}
