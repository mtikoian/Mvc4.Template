Mvc4 Template
------------------
New project
update nuget packages
Restart VS
PM > Initialize-Chewie
PM> Invoke-chewie
PM> install-package mvcscaffolding
restart vs

create a model
scaffold model
add link to header



LESS
----
Change the "Build Action" of all less files to "Content" from "None"


web.config
----------
  <system.web>
    <httpHandlers>
      <add verb="GET" path="*.js"
         type="System.Web.StaticFileHandler" />

  <system.webServer>
    <handlers>
      <remove name="JavascriptFileHandler"/>
      <add name="JavascriptFileHandler" verb="GET" path="*.js" resourceType="File" type="System.Web.StaticFileHandler" />

web.release.config
  <system.web>
    <customErrors mode="Off" xdt:Transform="Replace"></customErrors>
