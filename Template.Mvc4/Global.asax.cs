﻿using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.ApplicationServer.Http;
using Template.Mvc4.Models;
using System.Data.Entity;
using Template.Mvc4.Api;
using System.Diagnostics;

namespace Template.Mvc4
{
  // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
  // visit http://go.microsoft.com/?LinkId=9394801

  public class MvcApplication : System.Web.HttpApplication
  {
    public static void RegisterGlobalFilters(GlobalFilterCollection filters)
    {
      filters.Add(new HandleErrorAttribute());
    }

    public static void RegisterRoutes(RouteCollection routes)
    {

      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      // configure api service
      var config = new HttpConfiguration() { EnableTestClient = true };
      routes.MapServiceRoute<ContactsApi>("api/contacts", config);

      // configure mvc controllers
      routes.MapRoute(
          "Default", // Route name
          "{controller}/{action}/{id}", // URL with parameters
          new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
      );

    }

    protected void Application_Start()
    {
      ViewEngines.Engines.Add(new CustomRazorViewEngine());
      AreaRegistration.RegisterAllAreas();

      RegisterGlobalFilters(GlobalFilters.Filters);
      RegisterRoutes(RouteTable.Routes);

      Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TemplateMvc4Context>());
    }


    [Conditional("DEBUG")]
    private void SetDbInit()
    {
      Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TemplateMvc4Context>());
      Database.SetInitializer(new CreateDatabaseIfNotExists<TemplateMvc4Context>());
    }
  }
  public class CustomRazorViewEngine : RazorViewEngine
  {
    private static readonly string[] NewPartialViewFormats = new[] { "~/Views/_Shared/{0}.cshtml" };

    public CustomRazorViewEngine()
    {
      base.PartialViewLocationFormats = base.PartialViewLocationFormats.Union(NewPartialViewFormats).ToArray();
    }
  }
}