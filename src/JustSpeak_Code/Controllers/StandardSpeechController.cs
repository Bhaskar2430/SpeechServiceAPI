using JustSpeak.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

using JustSpeak.Interfaces;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace JustSpeak.Controllers
{
    public class StandardSpeechController : Controller
    {
        private readonly ILogger<StandardSpeechController> _logger;
        private readonly IServiceHelper _iServiceHelper;

        static string YourSubscriptionKey = "e76fc661b51448da96fb272453cdb23e";
        static string YourServiceRegion = "eastus";

        public StandardSpeechController(IServiceHelper serviceHelper, ILogger<StandardSpeechController> logger)
        {
            _logger = logger;
            _iServiceHelper = serviceHelper;
        }
        [HttpGet]
        public ActionResult Index()
        {
            ViewData["selectedStandard"] = "active";
            ViewData["ddlLegalNames"] = new SelectList(_iServiceHelper.GetEmpLegalNames(), "LegalNameId", "LegalNames");
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Index(string name)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001/");
                //readonly string uri = "https://localhost:2236/api/cars";
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // int UserId, string textSpeech, string phonetic
                string UserId = "100";
                string textSpeech = name;
                string phonetic = "jho";
                var postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("UserId", UserId));
                postData.Add(new KeyValuePair<string, string>("textSpeech", textSpeech));
                postData.Add(new KeyValuePair<string, string>("phonetic", phonetic));
                HttpContent content = new FormUrlEncodedContent(postData);
                //client.DefaultRequestHeaders.Add()

                HttpResponseMessage response = await client.GetAsync("https://localhost:5001/api/TextToSpeech?UserId=100&textSpeech=Joe&phonetic=JH%20AU");
                if (response.IsSuccessStatusCode)
                {
                    //var output = await response.Content;
                    //result = RenderRazorViewToString("~/Views/WebCMS/_ChannelLocations.cshtml", output.result);

                }
                //GET Method
                //HttpResponseMessage response = await client.GetAsync("/api/TexttoSpeech");
                ////if (response.IsSuccessStatusCode)
                ////{
                ////    Departmentdepartment = awaitresponse.Content.ReadAsAsync<Department>();
                ////    Console.WriteLine("Id:{0}\tName:{1}", department.DepartmentId, department.DepartmentName);
                ////    Console.WriteLine("No of Employee in Department: {0}", department.Employees.Count);
                ////}
                ////else
                ////{
                ////    Console.WriteLine("Internal server Error");
                ////}
            }

            return View();
        }
        //[HttpPost]
        //public ActionResult Index(int n1, int n2)
        //{
        //    int z = n1 + n2;
        //    //Console.WriteLine("The sum is " + z);
        //    return View();
        //}

    }
}