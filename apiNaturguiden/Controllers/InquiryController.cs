﻿using apiNaturguiden.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace apiNaturguiden.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class InquiryController : ApiController
    {
        InquiryHandler inquiryHandler;
        public InquiryController()
        {
            inquiryHandler = new InquiryHandler();
        }
        // GET: api/Inquiry
        [HttpGet]
        public libraryNaturguiden.Inquiry[] Get()
        {
            return inquiryHandler.GetInquiries();
        }

        // GET: api/Inquiry/5
        [HttpGet]
        public libraryNaturguiden.Inquiry Get(int id)
        {
            return inquiryHandler.GetInquiry(id);
        }

        // POST: api/Inquiry
        [HttpPost]
        public async void Post(libraryNaturguiden.Inquiry formData)
        {
            if(formData != null && ModelState.IsValid)
            {
                int status = await inquiryHandler.SaveInquiry(formData);
                if (status == 1)
                {
                    InquiryHandler.SendConfirmMail(formData);
                }
            }
        }

        // PUT: api/Inquiry/5
        [HttpPut]
        public async void Put(libraryNaturguiden.Inquiry formData)
        {
            if (formData != null)
            {
                await inquiryHandler.UpdateInquiry(formData);
            }
        }
        [HttpDelete]
        // DELETE: api/Inquiry/5
        public async void Delete(int id)
        {
            await inquiryHandler.DeleteInquiry(id);
        }
    }
}
