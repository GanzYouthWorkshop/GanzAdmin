using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using GanzAdmin.Database;
using System.Linq;
using System.Collections.Generic;
using GanzAdmin.Database.Models;
using GanzAdmin.UserMessages;

namespace GanzAdmin.API.DoorProxy
{
    public class DoorproxyApiController : Controller
    {
        private UserMessageService m_UserMessageService;

        public DoorproxyApiController(UserMessageService userMessageService)
        {
            this.m_UserMessageService = userMessageService;
        }

        [HttpGet]
        [Route("api/door-proxy/check-proxy")]
        public ContentResult CheckCode(string code)
        {
            string result = "UNKNOWN";

            try
            {
                if (!string.IsNullOrEmpty(code))
                {
                    this.m_UserMessageService.QueueMessage(new UserMessage
                    {
                        Severity = UserMessage.Severities.Success,
                        Type = "RFID",
                        Text = $"Leolvastak egy RFID kódot: {code}"
                    });

                    using (GanzAdminDbEngine db = GanzAdminDbEngine.GetInstance())
                    {
                        List<Member> findResults = db.Members.Find(member => member.RFID == code).ToList();
                        if (findResults.Count == 1)
                        {
                            result = "OK";
                        }
                        else
                        {
                            result = "INVALID";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                result = "INVALID";
            }

            return Content(result);
        }
    }
}
