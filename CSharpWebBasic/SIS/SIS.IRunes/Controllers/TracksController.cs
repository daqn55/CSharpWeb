using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.IRunes.Models;
using SIS.WebServer.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace SIS.IRunes.Controllers
{
    public class TracksController : BaseController
    {
        private Dictionary<string, object> viewBag;

        public TracksController()
        {
            this.viewBag = new Dictionary<string, object>();
        }

        public IHttpResponse TrackCreate(IHttpRequest request)
        {
            var albumId = new Guid(request.QueryData["albumId"].ToString());
            this.viewBag.Add("AlbumId", albumId);

            return this.View($"/Tracks/Create", viewBag, true);
        }

        public IHttpResponse DoTrack(IHttpRequest request)
        {
            var name = request.FormData["name"].ToString().Trim();
            var link = request.FormData["link"].ToString().Trim();
            var price = request.FormData["price"].ToString().Trim();
            var albumId = new Guid(request.QueryData["albumId"].ToString());

            var tryParsePrice = decimal.TryParse(price, out decimal parsedPrice);
            if (!tryParsePrice)
            {
                return this.BadRequestError("Please provide valid Price.");
            }

            var track = new Track
            {
                Name = name,
                Link = link,
                Price = parsedPrice,
            };
            this.dbContext.Albums.Find(albumId).Tracks.Add(track);

            try
            {
                this.dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("Can't save track in database!");
                Console.WriteLine(e.Message);
            }


            return new RedirectResult($"/Albums/Details?id={albumId}");
        }

        public IHttpResponse TrackDetails(IHttpRequest request)
        {
            var albumId = request.QueryData["albumId"].ToString();
            var trackId = new Guid(request.QueryData["trackId"].ToString());

            var track = this.dbContext.Tracks.Find(trackId);
            var decodedLink = HttpUtility.UrlDecode(track.Link);

            if (decodedLink.Contains("youtube"))
            {
                decodedLink = decodedLink.Replace("watch?v=", "embed/");
            }

            this.viewBag.Add("VideoLink", decodedLink);
            this.viewBag.Add("Name", track.Name);
            this.viewBag.Add("Price", track.Price.ToString("F2"));
            this.viewBag.Add("AlbumId", albumId);

            return this.View("/Tracks/Details", viewBag, true);
        }
    }
}
