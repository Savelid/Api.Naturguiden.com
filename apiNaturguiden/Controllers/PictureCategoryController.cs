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
    [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
    public class PictureCategoryController : ApiController
    {
        PictureHandler pictureHandler;
        public PictureCategoryController()
        {
            pictureHandler = new PictureHandler();
        }
        // GET: api/PictureCategory
        [HttpGet]
        public libraryNaturguiden.PictureCategory[] Get()
        {
            return pictureHandler.GetCategories();
        }

        // GET: api/PictureCategory/5
        [HttpGet]
        public libraryNaturguiden.PictureCategory Get(int id)
        {
            return pictureHandler.GetCategory(id);
        }

        // POST: api/PictureCategory
        [HttpPost]
        public void Post(libraryNaturguiden.PictureCategory category)
        {
            pictureHandler.AddCategory(category);
        }

        // PUT: api/PictureCategory/5
        [HttpPut]
        public void Put(libraryNaturguiden.PictureCategory category)
        {
            pictureHandler.EditCategory(category);
        }

        // DELETE: api/PictureCategory/5
        [HttpDelete]
        public void Delete(int id)
        {
            pictureHandler.RemoveCategory(id);
        }
    }
}
