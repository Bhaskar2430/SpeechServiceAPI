using JustSpeak.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using System.Text;

namespace JustSpeak.APIControllers
{
    [ApiController]
    [Route("api/[controller]")]
   
    public class SpeakController : ControllerBase
    {
        static string YourSubscriptionKey = "e76fc661b51448da96fb272453cdb23e";
        //static string YourServiceRegion = "eastus";

        [Route("AjaxMethod")]
        [HttpPost]
        public EmpLegalNames AjaxMethod(EmpLegalNames empLegalName)
        {
            //person.DateTime = DateTime.Now.ToString();
            return empLegalName;
        }
        [Route("TexttoSpeech")]
        [HttpGet]
        public ActionResult TexttoSpeech()
        {
            texttoSpeech("Nagaraju");
            return Ok();
        }
        private void texttoSpeech(string textToConvert)
        {

            string locale = "en-US";
            string voiceName = "en-US-JennyNeural";

            string outputFile = @"C:\Users\NagaRaju\source\repos\SpeechService\SpeechService\Files\";

            const string SsmlPattern = @"<speak version=""1.0"" xmlns=""http://www.w3.org/2001/10/synthesis"" 
                xmlns:mstts=""http://www.w3.org/2001/mstts"" xml:lang=""{0}"">" +
            @"<voice name = ""{1}"">{2}</voice>" +
            @"</speak>";



            string ssml = "";

            ssml = string.Format(SsmlPattern, locale, voiceName, textToConvert);

            // string token = APIHelper.   (issueTokenUrl, subscriptionKey);
            string token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJyZWdpb24iOiJlYXN0dXMiLCJzdWJzY3JpcHRpb24taWQiOiI2ZmFlNTM4MDc5MjM0YTE4OTAyMTg5NmNhNmM1MmE4MCIsInByb2R1Y3QtaWQiOiJTcGVlY2hTZXJ2aWNlcy5TMCIsImNvZ25pdGl2ZS1zZXJ2aWNlcy1lbmRwb2ludCI6Imh0dHBzOi8vYXBpLmNvZ25pdGl2ZS5taWNyb3NvZnQuY29tL2ludGVybmFsL3YxLjAvIiwiYXp1cmUtcmVzb3VyY2UtaWQiOiIvc3Vic2NyaXB0aW9ucy9kOGQzZjU1YS04Njc5LTRkYWQtYmJlMy0zYjg1MTEwMGM3OGUvcmVzb3VyY2VHcm91cHMvcmctQ3JhenlNaW5kcy9wcm92aWRlcnMvTWljcm9zb2Z0LkNvZ25pdGl2ZVNlcnZpY2VzL2FjY291bnRzL1NwZWVjaENvbnZlcnRpb24iLCJzY29wZSI6InNwZWVjaHNlcnZpY2VzIiwiYXVkIjoidXJuOm1zLnNwZWVjaHNlcnZpY2VzLmVhc3R1cyIsImV4cCI6MTY1MjQ0NzA2NywiaXNzIjoidXJuOm1zLmNvZ25pdGl2ZXNlcnZpY2VzIn0.D25_X1AGdwe9QgLMJerNMPxB5Uhsn0Avkc5f69_44mA";

            string endpointUrl = "https://eastus.tts.speech.microsoft.com/cognitiveservices/v1";

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(endpointUrl);
            //string ImpressionGUID = Guid.NewGuid().ToString();
            webRequest.ContentType = "application/ssml+xml";
            //webRequest.Headers.Add("X-MICROSOFT-OutputFormat", "riff-16khz-16bit-mono-pcm");
            webRequest.Headers.Add("X-MICROSOFT-OutputFormat", "audio-16khz-32kbitrate-mono-mp3");
            webRequest.Headers["Authorization"] = "Bearer " + token;

            webRequest.Headers.Add("Ocp-Apim-Subscription-Key", YourSubscriptionKey);
            //webRequest.Headers.Add("X-FD-ImpressionGUID", ImpressionGUID);
            webRequest.UserAgent = "TTSClient";
            webRequest.Method = "POST";


            byte[] btBodyS = Encoding.UTF8.GetBytes(ssml);
            webRequest.ContentLength = btBodyS.Length;
            webRequest.GetRequestStream().Write(btBodyS, 0, btBodyS.Length);
            webRequest.Timeout = 6000000;

            using (var response = webRequest.GetResponse() as HttpWebResponse)
            {
                var sstream = response.GetResponseStream();



                string path = Path.Combine(outputFile, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }


                using (FileStream stream = new FileStream(Path.Combine(path, "Test"), FileMode.Create))
                {
                    sstream.CopyTo(stream);
                }

                //using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                //{
                //    sstream.CopyTo(fs);
                //}
            }
        }
    
    }
}
