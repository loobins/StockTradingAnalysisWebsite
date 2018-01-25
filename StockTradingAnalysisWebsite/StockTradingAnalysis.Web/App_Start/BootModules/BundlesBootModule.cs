﻿using System.Web.Optimization;
using StockTradingAnalysis.Interfaces.Common;

namespace StockTradingAnalysis.Web.BootModules
{
    public class BundlesBootModule : IBootModule
    {
        public int Priority => 0;

        public void Boot()
        {
            RegisterBundles(BundleTable.Bundles);
        }

        private static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/Css/Site.css",
                "~/Content/Css/prettyPhoto.css",
                "~/Content/themes/dark-hive/jquery-ui.dark-hive.css"));

            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                "~/Scripts/jquery.dateandtimepicker.js",
                "~/Scripts/custom.js",
                "~/Scripts/jquery.prettyPhoto.js"));

            bundles.Add(new ScriptBundle("~/bundles/validation").Include(
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js",
                "~/Scripts/validate.decimal.fix.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                "~/Scripts/jquery-ui-{version}.js"));

            BundleTable.EnableOptimizations = false;
        }
    }
}