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
    public class NewsController : ApiController
    {
        NewsHandler newsHandler;
        public NewsController()
        {
            newsHandler = new NewsHandler();
        }
        // GET: api/News
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        public libraryNaturguiden.News[] Get()
        {
            var news = newsHandler.GetNews();
            return news.Where(x => x.Position > 0).ToArray();
        }

        // GET: api/News/5
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        public libraryNaturguiden.News Get(int id)
        {
            return newsHandler.GetNews(id);
        }

        [EnableCors(origins: "http://admin.naturguiden.com", headers: "*", methods: "*")]
        // POST: api/News
        [HttpPost]
        public void Post(libraryNaturguiden.News news)
        {
            newsHandler.AddNews(news);
        }

        [EnableCors(origins: "http://admin.naturguiden.com", headers: "*", methods: "*")]
        // PUT: api/News
        [HttpPut]
        public void Put(libraryNaturguiden.News news)
        {
            newsHandler.EditNews(news);
        }

        [EnableCors(origins: "http://admin.naturguiden.com", headers: "*", methods: "*")]
        // DELETE: api/News/5
        [HttpDelete]
        public void Delete(int id)
        {
            newsHandler.RemoveNews(id);
        }
    }
}
