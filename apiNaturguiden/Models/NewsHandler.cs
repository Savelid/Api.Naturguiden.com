using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using libraryNaturguiden;

namespace apiNaturguiden.Models
{
    public class NewsHandler
    {
        NewsEntities db;
        public NewsHandler()
        {
            db = new NewsEntities();
        }
        internal libraryNaturguiden.News[] GetNews()
        {
            var value = db.News
                .OrderBy(x => x.Position)
                .OrderBy(x => x.Date)
                .Select(x => new libraryNaturguiden.News
                {
                    Id = x.Id,
                    Date = x.Date,
                    Creator = x.Creator,
                    Heading = x.Heading,
                    Text = x.Text,
                    PictureId = x.Picture == null ? 0 : (int)x.Picture,
                    LinkUrl = x.LinkUrl,
                    LinkText = x.LinkText,
                    Position = x.Position
                }).ToArray();

            var pictureHandler = new PictureHandler();
            foreach (var item in value)
            {
                item.Picture = pictureHandler.GetPicture(item.PictureId);
            }
            return value;
        }

        internal libraryNaturguiden.News GetNews(int id)
        {
            var value = db.News
                .Where(x => x.Id == id)
                .Select(x => new libraryNaturguiden.News
                {
                    Id = x.Id,
                    Date = x.Date,
                    Creator = x.Creator,
                    Heading = x.Heading,
                    Text = x.Text,
                    PictureId = x.Picture == null ? 0 : (int)x.Picture,
                    LinkUrl = x.LinkUrl,
                    LinkText = x.LinkText,
                    Position = x.Position
                }).FirstOrDefault();

            var pictureHandler = new PictureHandler().GetPicture(value.PictureId);
            value.Picture = pictureHandler;
            return value;

        }

        internal void AddNews(libraryNaturguiden.News news)
        {
            var newsToAdd = new News
            {
                Id = news.Id,
                Date = news.Date,
                Creator = news.Creator,
                Heading = news.Heading,
                Text = news.Text,
                Picture = news.PictureId,
                LinkUrl = news.LinkUrl,
                LinkText = news.LinkText,
                Position = news.Position
            };
            db.News.Add(newsToAdd);
            db.SaveChanges();
        }

        internal void EditNews(libraryNaturguiden.News news)
        {
            var newsToAdd = db.News.Where(x => x.Id == news.Id).FirstOrDefault();
            if(newsToAdd != null)
            {
                newsToAdd.Id = news.Id;
                newsToAdd.Date = news.Date;
                newsToAdd.Creator = news.Creator;
                newsToAdd.Heading = news.Heading;
                newsToAdd.Text = news.Text;
                newsToAdd.Picture = news.PictureId;
                newsToAdd.LinkUrl = news.LinkUrl;
                newsToAdd.LinkText = news.LinkText;
                newsToAdd.Position = news.Position;
                db.SaveChanges();
            }
        }

        internal void RemoveNews(int id)
        {
            db.News.Remove(db.News.Where(x => x.Id == id).FirstOrDefault());
        }
    }
}