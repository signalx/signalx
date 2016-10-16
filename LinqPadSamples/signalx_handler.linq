<Query Kind="Program">
  <NuGetReference Version="3.0.1">Microsoft.Owin.Host.HttpListener</NuGetReference>
  <NuGetReference Version="2.1.0">Microsoft.Owin.Hosting</NuGetReference>
  <NuGetReference Prerelease="true">SignalX</NuGetReference>
  <Namespace>Microsoft.Owin.Hosting</Namespace>
  <Namespace>Owin</Namespace>
  <Namespace>SignalXLib.Lib</Namespace>
  <Namespace>System</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

   public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.UseSignalX(new SignalX(""));
		}
	}
	internal class Program
	{
		private static void Main(string[] args)
		{
			var url = "http://localhost:44111";
			using (WebApp.Start<Startup>(url))
			{
			  var index = @"<!DOCTYPE html>
							<html>
							<body>
							<input id='message' type='text'/>
							<button id='send'>Send Message To Server</button>
							<button id='clear'>Clear Console</button>
							<div id='myconsole'></div>
							<script src='https://ajax.aspnetcdn.com/ajax/jquery/jquery-1.9.0.min.js'></script>     
							<script src='https://ajax.aspnetcdn.com/ajax/signalr/jquery.signalr-2.2.0.js'></script>
							<script src='https://unpkg.com/signalx'></script>
							<script>
							    var writeToMyConsole = function(m) {
							        $('#myconsole').append('<BR /><BR />'+m);
							    };
							    signalx.debug(function (o) { writeToMyConsole(o); });
							    signalx.error(function (o) { writeToMyConsole(o); });
							    $('#clear').on('click', function () {
							        $('#myconsole').html('--cleared--');
							    });
								signalx.client.handler=function (m) {
							            writeToMyConsole(m);
							    };
							    $('#send').on('click', function () {
							        var message = $('#message').val();
							       var promise = signalx.server.sample('MY MESSAGE','handler');
							    });
							</script>
							</body>
							</html>";
							
				var filePath=AppDomain.CurrentDomain.BaseDirectory+"\\index.html";
			    System.IO.File.WriteAllText(filePath,index);
				SignalX.Server("Sample", (request) => request.Respond(request.ReplyTo));
				System.Diagnostics.Process.Start(url);
				Console.ReadLine();
			}
		}
	}