using apiNaturguiden.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace apiNaturguiden.Controllers
{
    [EnableCors(origins: "http://localhost:8080", headers:"*", methods: "*")]
    public class PictureController : ApiController
    {
        PictureHandler pictureHandler; 
        public PictureController()
        {
            pictureHandler = new PictureHandler();
        }
        // GET: api/Picture
        [HttpGet]
        public libraryNaturguiden.Picture[] Get()
        {
            return pictureHandler.GetPictures();
        }

        // GET: api/Picture/5
        [HttpGet]
        public libraryNaturguiden.Picture Get(int id)
        {
            return pictureHandler.GetPicture(id);
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
