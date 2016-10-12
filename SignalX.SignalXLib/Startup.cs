using System;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;

namespace SignalXLib.Lib
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Turn cross domain on 
            var config = new HubConfiguration {EnableDetailedErrors = true, EnableJSONP = true};
            app.MapSignalR(config);

            var fileSystem = new PhysicalFileSystem(SignalX.UiFolder);
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