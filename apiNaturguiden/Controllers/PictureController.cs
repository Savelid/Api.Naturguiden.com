using apiNaturguiden.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace apiNaturguiden.Controllers
{
    [EnableCors(origins: "*", headers:"*", methods: "*")]
    public class PictureController : ApiController
    {
        PictureHandler pictureHandler; 
        public PictureController()
        {
            pictureHandler = new PictureHandler();
        }

        // GET: api/Picture
        [HttpGet]
        public libraryNaturguiden.PictureContainer Get()
        {
            return new libraryNaturguiden.PictureContainer(pictureHandler.GetPictures());
        }

        // GET: api/Picture/5
        [HttpGet]
        public libraryNaturguiden.PictureContainer Get(int id)
        {
            return new libraryNaturguiden.PictureContainer(pictureHandler.GetPicture(id));
        }

        // GET: api/Picture/Album
        [HttpGet]
        [Route("api/Picture/Album")]
        public libraryNaturguiden.PictureContainer Album()
        {
            return new libraryNaturguiden.PictureContainer(pictureHandler.GetAlbumPictures());
        }

        // GET: api/Picture/Album/Skiing
        [HttpGet]
        [Route("api/Picture/Album/{category}")]
        public libraryNaturguiden.PictureContainer Album(string category)
        {
            return new libraryNaturguiden.PictureContainer(pictureHandler.GetAlbumPictures(category));
        }

        // POST: api/Picture
        [HttpPost]
        public void Post(libraryNaturguiden.Picture picture)
        {
            pictureHandler.AddPicture(picture);
        }

        // PUT: api/Picture
        [HttpPut]
        public void Put(libraryNaturguiden.Picture picture)
        {
            pictureHandler.EditPicture(picture);
        }

        // DELETE: api/Picture/5
        [HttpDelete]
        public void Delete(int id)
        {
            pictureHandler.RemovePicture(id);
        }
    }
}
