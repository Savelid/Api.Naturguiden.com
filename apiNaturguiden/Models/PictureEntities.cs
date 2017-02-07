using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace apiNaturguiden.Models
{
    public partial class PictureEntities
    {
        public Object GetPicture(int id)
        {
            return Picture
                .Where(x => x.Id == id)
                .Select(x => new {
                    Url = x.Url,
                    Owner = x.Owner,
                    Date = x.Date }).FirstOrDefault();
        }
    }
}