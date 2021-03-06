﻿using System.Web;
using System.Web.Optimization;

namespace rad301_ca2_S00140633
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Content/amcharts/amcharts.js",
                "~/Content/amcharts/pie.js",
                "~/Content/amcharts/themes/light.js",
                "~/Scripts/jquery.blockUI.js",
                "~/Scripts/jquery.rateit.js",
                         "~/Scripts/toastr.js"
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
                "~/Content/sandstone.bootstrap.css",
                "~/Content/toastr.css",
                "~/Content/site.css"));
        }
    }
}
