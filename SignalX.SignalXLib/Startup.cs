using System;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;

namespace SignalX.SignalXLib
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Turn cross domain on 
            var config = new HubConfiguration {EnableDetailedErrors = true, EnableJSONP = true};
            app.MapSignalR(config);

            var fileSystem = new PhysicalFileSystem(AppDomain.CurrentDomain.BaseDirectory + "/ui");
            var options = new FileServerOptions
            {
                EnableDirectoryBrowsing = true,
                FileSystem = fileSystem,
                EnableDefaultFiles = true
            };

            app.UseFileServer(options);
        }
    }
}