using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using libraryNaturguiden;

namespace apiNaturguiden.Models
{
    public class PictureHandler
    {
        const string UrlBase = "http://localhost:9946/";
        PictureEntities db;
        public PictureHandler()
        {
            db = new PictureEntities();
        }

        public libraryNaturguiden.Picture[] GetPictures()
        {
            return db.Picture
                .Select(x => new libraryNaturguiden.Picture {
                    Id = x.Id,
                    Url = x.Url.Contains("http://") ? x.Url : UrlBase + x.Url,
                    Alt = x.Alt,
                    Date = x.Date,
                    Owner = x.Owner,
                    Format = x.Format,
                    Categories = x.Category.Select(y => y.Id).ToList()
                } )
                .ToArray();
        }

        public libraryNaturguiden.Picture GetPicture(Int32 id)
        {
            return db.Picture
                .Where(x => x.Id == id)
                .Select(x => new libraryNaturguiden.Picture
                {
                    Id = x.Id,
                    Url = x.Url.Contains("http://") ? x.Url : UrlBase + x.Url,
                    Alt = x.Alt,
                    Date = x.Date,
                    Owner = x.Owner,
                    Format = x.Format,
                    Categories = x.Category.Select(y => y.Id).ToList()
                })
                .FirstOrDefault();
        }

        public void AddPicture(libraryNaturguiden.Picture newPicture)
        {
            var picture = new Picture
            {
                Alt = newPicture.Alt,
                Date = newPicture.Date,
                Owner = newPicture.Owner,
                Url = newPicture.Url,
                Format = newPicture.Format,
                Category = newPicture.Categories == null ? null : db.Category.Where(x => newPicture.Categories.Contains(x.Id)).ToList()
        };
            db.Picture.Add(picture);
            db.SaveChanges();
        }

        public void EditPicture(libraryNaturguiden.Picture picture)
        {
            var dbPicture = db.Picture.Where(x => x.Id == picture.Id).FirstOrDefault();
            dbPicture.Alt = picture.Alt;
            dbPicture.Date = picture.Date;
            dbPicture.Owner = picture.Owner;
            dbPicture.Url = picture.Url;
            dbPicture.Format = picture.Format;
            dbPicture.Category = picture.Categories == null ? null : db.Category.Where(x => picture.Categories.Contains(x.Id)).ToList();
            db.SaveChanges();
        }

        public void RemovePicture(int id)
        {
            db.Picture.Remove(db.Picture.Where(x => x.Id == id).FirstOrDefault());
            db.SaveChanges();
        }

        public libraryNaturguiden.PictureCategory[] GetCategories()
        {
            return db.Category
                .Select(x => new libraryNaturguiden.PictureCategory
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToArray();
        }

        public libraryNaturguiden.PictureCategory GetCategory(int id)
        {
            return db.Category
                .Where(x => x.Id == id)
                .Select(x => new libraryNaturguiden.PictureCategory
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .FirstOrDefault();
        }

        public void AddCategory(libraryNaturguiden.PictureCategory newCategory)
        {
            var category = new Category
            {
                Id = newCategory.Id,
                Name = newCategory.Name
            };
            db.Category.Add(category);
            db.SaveChanges();
        }

        public void EditCategory(libraryNaturguiden.PictureCategory category)
        {
            var dbCategory = db.Category.Where(x => x.Id == category.Id).FirstOrDefault();
            dbCategory.Name = category.Name;
            db.SaveChanges();
        }

        public void RemoveCategory(int id)
        {
            db.Category.Remove(db.Category.Where(x => x.Id == id).FirstOrDefault());
            db.SaveChanges();
        }
    }
}