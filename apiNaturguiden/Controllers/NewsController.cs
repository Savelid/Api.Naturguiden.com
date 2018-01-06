using apiNaturguiden.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using libraryNaturguiden;
using System.Web.Http.Cors;

namespace apiNaturguiden.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class NewsController : ApiController
    {
        NewsHandler newsHandler;
        public NewsController()
        {
            newsHandler = new NewsHandler();
        }
        // GET: api/Picture
        [HttpGet]
        public libraryNaturguiden.News[] Get()
        {
            return newsHandler.GetNews();
        }

        // GET: api/Picture/5
        [HttpGet]
        public libraryNaturguiden.News Get(int id)
        {
            return newsHandler.GetNews(id);
        }

        // POST: api/Picture
        [HttpPost]
        public void Post(libraryNaturguiden.News news)
        {
            try
            {
                newsHandler.AddNews(news);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // PUT: api/Picture
        [HttpPut]
        public void Put(libraryNaturguiden.News news)
        {
            newsHandler.EditNews(news);
        }

        // DELETE: api/Picture/5
        [HttpDelete]
        public void Delete(int id)
        {
            newsHandler.RemoveNews(id);
        }
    }
}
