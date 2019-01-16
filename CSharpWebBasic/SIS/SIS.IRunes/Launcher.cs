using SIS.HTTP.Enums;
using SIS.IRunes.Controllers;
using SIS.WebServer;
using SIS.WebServer.Routing;

namespace SIS.IRunes
{
    public class Launcher
    {
        static void Main(string[] args)
        {
            ServerRoutingTable serverRoutingTable = new ServerRoutingTable();

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/"] = request => new HomeController().Index(request);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/Users/Login"] = request => new UserController().Login();
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/Users/Register"] = request => new UserController().Register();
            serverRoutingTable.Routes[HttpRequestMethod.Post]["/Users/Login"] = request => new UserController().DoLogin(request);
            serverRoutingTable.Routes[HttpRequestMethod.Post]["/Users/Register"] = request => new UserController().DoRegister(request);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/Logout"] = request => new UserController().Logout(request);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/Albums/All"] = request => new AlbumsController().AllAlbums(request);
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/Albums/Create"] = request => new AlbumsController().Create();
            serverRoutingTable.Routes[HttpRequestMethod.Post]["/Albums/Create"] = request => new AlbumsController().DoCreate(request);
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/Albums/Details"] = request => new AlbumsController().AlbumDetails(request);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/Tracks/Create"] = request => new TracksController().TrackCreate(request);
            serverRoutingTable.Routes[HttpRequestMethod.Post]["/Tracks/Create"] = request => new TracksController().DoTrack(request);
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/Tracks/Details"] = request => new TracksController().TrackDetails(request);


            Server server = new Server(80, serverRoutingTable);

            server.Run();
        }
    }
}
