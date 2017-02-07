using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using libraryNaturguiden;

namespace apiNaturguiden.Models
{
    public class PictureHandler
    {
        PictureEntities _db;
        public PictureHandler()
        {
            _db = new PictureEntities();
        }

        public libraryNaturguiden.Picture[] GetPictures()
        {
            return _db.Picture
                .Select(x => new libraryNaturguiden.Picture {
                    Id = x.Id,
                    Url = x.Url,
                    Alt = x.Alt,
                    Date = x.Date,
                    Owner = x.Owner,
                    Categories = x.Category.Select(y => y.Id).ToList()
                } )
                .ToArray();
        }

        public libraryNaturguiden.Picture GetPicture(int id)
        {
            return _db.Picture
                .Where(x => x.Id == id)
                .Select(x => new libraryNaturguiden.Picture
                {
                    Id = x.Id,
                    Url = x.Url,
                    Alt = x.Alt,
                    Date = x.Date,
                    Owner = x.Owner,
                    Categories = x.Category.Select(y => y.Id).ToList()
                })
                .FirstOrDefault();
        }

        public void AddPicture(libraryNaturguiden.Picture newPicture)
        {
            var picture = new Picture
            {
                Id = newPicture.Id,
                Alt = newPicture.Alt,
                Date = newPicture.Date,
                Owner = newPicture.Owner,
                Url = newPicture.Url,
                Category = _db.Category.Where(x => newPicture.Categories.Contains(x.Id)).ToList()
            };
            _db.Picture.Add(picture);
            _db.SaveChanges();
        }

        public void EditPicture(libraryNaturguiden.Picture picture)
        {
            var dbPicture = _db.Picture.Where(x => x.Id == picture.Id).FirstOrDefault();
            dbPicture.Alt = picture.Alt;
            dbPicture.Date = picture.Date;
            dbPicture.Owner = picture.Owner;
            dbPicture.Url = picture.Url;
            dbPicture.Category = _db.Category.Where(x => picture.Categories.Contains(x.Id)).ToList();
            _db.SaveChanges();
        }

        public void RemovePicture(int id)
        {
            _db.Picture.Remove(_db.Picture.Where(x => x.Id == id).FirstOrDefault());
            _db.SaveChanges();
        }

        public libraryNaturguiden.PictureCategory[] GetCategories()
        {
            return _db.Category
                .Select(x => new libraryNaturguiden.PictureCategory
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToArray();
        }

        public libraryNaturguiden.PictureCategory GetCategory(int id)
        {
            return _db.Category
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
            _db.Category.Add(category);
            _db.SaveChanges();
        }

        public void EditCategory(libraryNaturguiden.PictureCategory category)
        {
            var dbCategory = _db.Category.Where(x => x.Id == category.Id).FirstOrDefault();
            dbCategory.Name = category.Name;
            _db.SaveChanges();
        }

        public void RemoveCategory(int id)
        {
            _db.Category.Remove(_db.Category.Where(x => x.Id == id).FirstOrDefault());
            _db.SaveChanges();
        }
    }
}