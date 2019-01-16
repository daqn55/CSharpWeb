using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.IRunes.Models;
using SIS.WebServer.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SIS.IRunes.Controllers
{
    public class AlbumsController : BaseController
    {
        public IHttpResponse AllAlbums(IHttpRequest request)
        {
            var viewBag = new Dictionary<string, object>();

            var albums = this.dbContext.Albums;

            if (albums.Any())
            {
                var listOfAlbums = string.Empty;
                foreach (var a in albums)
                {
                    var albumHtml = $@"<p><a href=""/Albums/Details?id={a.Id}"">{a.Name}</a></p>";
                    listOfAlbums += albumHtml;
                }

                viewBag.Add("AllAlbums", listOfAlbums);

                return this.View("Albums/All", viewBag, true);
            }

            viewBag.Add("AllAlbums", "There are currently no albums.");
            return View("Albums/All", viewBag, true);
        }

        public IHttpResponse Create()
        {
            return View("Albums/Create", null, true);
        }

        public IHttpResponse DoCreate(IHttpRequest request)
        {
            var name = request.FormData["name"].ToString().Trim();
            var cover = request.FormData["cover"].ToString().Trim();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name))
            {
                return this.BadRequestError("Please provide a valid Name.");
            }

            if (string.IsNullOrEmpty(cover) || string.IsNullOrWhiteSpace(cover))
            {
                return this.BadRequestError("Please provide a valid Link for Cover.");
            }

            var album = new Album
            {
                Name = name,
                Cover = cover
            };

            this.dbContext.Albums.Add(album);

            try
            {
                this.dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("Can't save in database!");
                Console.WriteLine(e.Message);
            }

            return new RedirectResult("/Albums/All");
        }

        public IHttpResponse AlbumDetails(IHttpRequest request)
        {
            var viewBag = new Dictionary<string, object>();
            var idOfAlbum = new Guid(request.QueryData.FirstOrDefault(x => x.Key == "id").Value.ToString());

            if (idOfAlbum != null)
            {
                var album = this.dbContext.Albums.Find(idOfAlbum);
                var urlDecode = HttpUtility.UrlDecode(album.Cover);
                viewBag.Add("Image", urlDecode);
                viewBag.Add("Name", album.Name);
                viewBag.Add("Price", album.Price.ToString("F2"));
                viewBag.Add("AlbumId", album.Id);

                var listOfTracks = string.Empty;
                foreach (var t in album.Tracks)
                {
                    var trackHtml = $@"<li><a href=""/Tracks/Details?albumId={album.Id}&trackId={t.Id}""><i>{t.Name}</i></a></li>";
                    listOfTracks += trackHtml;
                }
                viewBag.Add("AllTracks", listOfTracks);

                return this.View("/Albums/Details", viewBag, true);
            }

            return new RedirectResult("/Albums/All");
        }
    }
}
