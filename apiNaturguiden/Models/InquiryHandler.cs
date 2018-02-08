using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace apiNaturguiden.Models
{
    public class InquiryHandler
    {
        InquiryEntities db;
        public InquiryHandler()
        {
            db = new InquiryEntities();
        }

        public async Task<int> SaveInquiry(libraryNaturguiden.Inquiry formData)
        {
            this.db.Inquiry.Add(new Inquiry {
                StartDate = formData.StartDate,
                EndDate = formData.EndDate,
                NumberOfPeople = formData.NumberOfPeople,
                Category = formData.Category,
                SubCategory = formData.SubCategory,
                Comment = formData.Comment,
                FirstName = formData.FirstName,
                LastName = formData.LastName,
                Phone = formData.Phone,
                Country = formData.Country,
                Email = formData.Email
            });
            int status = await this.db.SaveChangesAsync();
            return status;
        }

        public async Task<int> UpdateInquiry(libraryNaturguiden.Inquiry formData)
        {
            var value = this.db.Inquiry.Where(x => x.Id == formData.Id).FirstOrDefault();
            if(value != null)
            {
                value.StartDate = formData.StartDate;
                value.EndDate = formData.EndDate;
                value.NumberOfPeople = formData.NumberOfPeople;
                value.Category = formData.Category;
                value.SubCategory = formData.SubCategory;
                value.Comment = formData.Comment;
                value.FirstName = formData.FirstName;
                value.LastName = formData.LastName;
                value.Phone = formData.Phone;
                value.Country = formData.Country;
                value.Email = formData.Email;
            }
            int status = await this.db.SaveChangesAsync();
            return status;
        }

        public libraryNaturguiden.Inquiry GetInquiry(int id)
        {
            var value = this.db.Inquiry.Where(x => x.Id == id).Select(formData => new libraryNaturguiden.Inquiry
            {
                Id = formData.Id,
                StartDate = formData.StartDate,
                EndDate = formData.EndDate,
                NumberOfPeople = formData.NumberOfPeople,
                Category = formData.Category,
                SubCategory = formData.SubCategory,
                Comment = formData.Comment,
                FirstName = formData.FirstName,
                LastName = formData.LastName,
                Phone = formData.Phone,
                Country = formData.Country,
                Email = formData.Email
            }).FirstOrDefault();
            return value;
        }

        public libraryNaturguiden.Inquiry[] GetInquiries()
        {
            var value = this.db.Inquiry.Select(formData => new libraryNaturguiden.Inquiry
            {
                Id = formData.Id,
                StartDate = formData.StartDate,
                EndDate = formData.EndDate,
                NumberOfPeople = formData.NumberOfPeople,
                Category = formData.Category,
                SubCategory = formData.SubCategory,
                Comment = formData.Comment,
                FirstName = formData.FirstName,
                LastName = formData.LastName,
                Phone = formData.Phone,
                Country = formData.Country,
                Email = formData.Email
            }).ToArray();
            return value;
        }

        public async Task<int> DeleteInquiry(int id)
        {
            this.db.Inquiry.Remove(this.db.Inquiry.Where(x => x.Id == id).FirstOrDefault());
            int status = await this.db.SaveChangesAsync();
            return status;
        }

        public static void SendConfirmMail(libraryNaturguiden.Inquiry formData)
        {
            SmtpClient smtpClient = new SmtpClient(libraryNaturguiden.Secret.NaturguidenEmailHost, libraryNaturguiden.Secret.NaturguidenEmailPort);
            //smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential(libraryNaturguiden.Secret.NaturguidenEmail, libraryNaturguiden.Secret.NaturguidenEmailPassword);

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(libraryNaturguiden.Secret.NaturguidenEmail, "Naturguiden");
            mail.To.Add(new MailAddress(formData.Email));
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            mail.ReplyToList.Add(new MailAddress(libraryNaturguiden.Secret.NaturguidenEmail, "Naturguiden"));

            mail.Subject = $"Naturguiden Confirmation";

            string format = "<tr style='background-color: {0}'><td style='padding: 8px;'>{1}</td><td colspan=2 style='padding: 8px;'>{2}</td></tr>";
            string singleColRow = "<tr><td colspan=3>{0}</td></tr>";
            string tableColor1 = "#EEEEEE";
            string tableColor2 = "#FFFFFF";
            StringBuilder mailHtmlBody = new StringBuilder();
            mailHtmlBody.Append("<table>");
            mailHtmlBody.Append($"<tr><td colspan=2><h2>Request from {formData.FirstName} from naturguiden.com</h2></td>");
            mailHtmlBody.Append("<tr><td colspan=3 style='border-bottom: 1px solid #EEEEEE; height: 30px;'></td></tr>");
            mailHtmlBody.AppendFormat(singleColRow, "<h4>Summary</h4>");
            mailHtmlBody.Append($"<tr style='background-color: {tableColor2}'><td colspan=3 style='padding: 8px;'>Message:</td></tr>");
            mailHtmlBody.Append($"<tr style='background-color: {tableColor2}'><td colspan=3 style='padding: 8px;'>{formData.Comment}</td></tr>");
            mailHtmlBody.AppendFormat(format, tableColor1, "Start Date", formData.StartDate.HasValue ? formData.StartDate.Value.ToShortDateString() : "None");
            mailHtmlBody.AppendFormat(format, tableColor2, "End Date", formData.EndDate.HasValue ? formData.EndDate.Value.ToShortDateString() : "None");
            mailHtmlBody.AppendFormat(format, tableColor1, "Nr of people", formData.NumberOfPeople.HasValue ? formData.NumberOfPeople.Value : 0);
            mailHtmlBody.AppendFormat(format, tableColor2, "Category", formData.Category);
            mailHtmlBody.AppendFormat(format, tableColor1, "Sub-Category", formData.SubCategory);
            mailHtmlBody.AppendFormat(format, tableColor2, "First Name", formData.FirstName);
            mailHtmlBody.AppendFormat(format, tableColor1, "Last Name", formData.LastName);
            mailHtmlBody.AppendFormat(format, tableColor2, "Phone", formData.Phone);
            mailHtmlBody.AppendFormat(format, tableColor1, "Email", formData.Email);
            mailHtmlBody.AppendFormat(format, tableColor2, "Country", formData.Country);
            mailHtmlBody.Append("<tr><td colspan=3 style='border-top: 1px solid #EEEEEE; height: 30px;'></td></tr>");
            mailHtmlBody.AppendFormat(singleColRow, "If you have any further querstions you are more than welcome to contact us");
            mailHtmlBody.Append("<td colspan=2>John Savelid at Naturguiden<br/>john@naturguiden.com<br/>+46 70 53 53 630</td></tr>");
            mailHtmlBody.AppendFormat(singleColRow, "<a href='http://www.naturguiden.com'>http://www.naturguiden.com</a>");
            mailHtmlBody.Append("</table>");
            mailHtmlBody.AppendLine();

            StringBuilder mailPlainBody = new StringBuilder();
            mailPlainBody.Append($"Thank you {formData.FirstName}!{Environment.NewLine}");
            mailPlainBody.Append($"We have recived your inquiry - {formData.Category} {formData.SubCategory} - and will get back to you soon.{Environment.NewLine}");
            mailPlainBody.Append(Environment.NewLine);
            mailPlainBody.Append("If you have any further querstions you are more than welcome to contact us" + Environment.NewLine);
            mailPlainBody.Append("John Savelid at Naturguiden" + Environment.NewLine);
            mailPlainBody.Append("john@naturguiden.com" + Environment.NewLine);
            mailPlainBody.Append("+46 70 53 53 630" + Environment.NewLine);
            mailPlainBody.Append(Environment.NewLine);
            mailPlainBody.Append("We at Naturguiden wish you a great day" + Environment.NewLine);
            mailPlainBody.Append(Environment.NewLine);
            mailPlainBody.Append("http://www.naturguiden.com" + Environment.NewLine);
            mailPlainBody.AppendLine();

            var plainView = AlternateView.CreateAlternateViewFromString(mailPlainBody.ToString(), null, "text/plain");
            var htmlView = AlternateView.CreateAlternateViewFromString(mailHtmlBody.ToString(), null, "text/html");
            mail.AlternateViews.Add(plainView);

            //Send mail to client
            smtpClient.Send(mail);

            mail.AlternateViews.Add(htmlView);
            mail.To.Clear();
            mail.To.Add(new MailAddress("john@naturguiden.com"));

            //Send simular mail to john
            smtpClient.Send(mail);
        }

    }
}