using CleanArc.Application.Interfaces;
using CleanArc.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Services
{
    public class SpeechService : ISpeechService
    {
        private string GetToken(string issueTokenUrl, string subscriptionKey)
        {
            var ibizaStsUrl = new Uri(issueTokenUrl);
            var authentication = new Authentication(ibizaStsUrl, subscriptionKey);
            return authentication.RetrieveNewTokenAsync();
        }

        public async Task<Responsemessage<TextToSpeechResponse>> TextToSpeech(int UserId, string textSpeech, string phonetic)
        {
            string subscriptionKey = "c701126198984a7685416c8d521b6d0b";
            string issueTokenUrl = "https://eastus.api.cognitive.microsoft.com/sts/v1.0/issueToken";
            string ssml = "";
            string locale = "en-US";
            string voiceName = "en-US-JennyNeural";
            string outputFile = @"C:\Users\kUrmika\Azure\potmanout";

            var response = new Responsemessage<TextToSpeechResponse>();
            TextToSpeechResponse textToSpeechResponse = new TextToSpeechResponse() { TextToSpeechViewModel = new List<TextToSpeechViewModel>() };

            var FullTextToSpeechViewModel = new List<TextToSpeechViewModel>()
                {
                    new TextToSpeechViewModel() { id = 100, Name = "Bhaskar" ,FilePath = @"\\Azure\potmanout\Test.mp3"},
                    new TextToSpeechViewModel() { id = 200, Name = "Sravan" ,FilePath = @"\\Azure\potmanout\Test.mp3"},
                    new TextToSpeechViewModel() { id = 300, Name = "Naga raju",FilePath = @"\\Azure\potmanout\Test.mp3"},
                    new TextToSpeechViewModel() { id = 400, Name = "Rajesh" ,FilePath = @"\\Azure\potmanout\Test.mp3"},
                    new TextToSpeechViewModel() { id = 500, Name = "Siva",FilePath = @"\\Azure\potmanout\Test.mp3"},
                };

            var TextToSpeechViewModelList = FullTextToSpeechViewModel.Find(p => p.id == UserId);

            textToSpeechResponse.TextToSpeechViewModel.Add(TextToSpeechViewModelList);
            
            response.Data = textToSpeechResponse;
                    
            if(response.Data != null)
            {
                response.ResponseStatus = new ResponseStatus() { Code = ErrorCode.Success, Status = true };
            }
            const string SsmlPattern = @"<speak version=""1.0"" xmlns=""http://www.w3.org/2001/10/synthesis"" 
                xmlns:mstts=""http://www.w3.org/2001/mstts"" xml:lang=""{0}"">" + @"<voice name = ""{1}"">{2}</voice>" +
            @"</speak>";

            string ups = "ups";
            StringBuilder builder = new StringBuilder();
            builder.Append("<s>His name is <phoneme alphabet=");
            builder.Append("'");
            builder.Append(ups);
            builder.Append("'");
            builder.Append(" ph=");
            builder.Append("'");
            builder.Append(phonetic);
            builder.Append("'");
            builder.Append(">");
            builder.Append(textSpeech);
            builder.Append("</phoneme></s>");


            ssml = string.Format(SsmlPattern, locale, voiceName, builder.ToString());

            string token = GetToken(issueTokenUrl, subscriptionKey);

            string endpointUrl = "https://eastus.tts.speech.microsoft.com/cognitiveservices/v1";

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(endpointUrl);
            webRequest.ContentType = "application/ssml+xml";
            //webRequest.Headers.Add("X-MICROSOFT-OutputFormat", "riff-16khz-16bit-mono-pcm");
            webRequest.Headers.Add("X-MICROSOFT-OutputFormat", "audio-16khz-32kbitrate-mono-mp3");  
            webRequest.Headers["Authorization"] = "Bearer " + token;
            webRequest.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
            webRequest.UserAgent = "TTSClient";
            webRequest.Method = "POST";

            byte[] btBodyS = Encoding.UTF8.GetBytes(ssml);
            webRequest.ContentLength = btBodyS.Length;
            webRequest.GetRequestStream().Write(btBodyS, 0, btBodyS.Length);
            webRequest.Timeout = 6000000;

            using (var response1 = webRequest.GetResponse() as HttpWebResponse)
            {
                var sstream = response1.GetResponseStream();
                using (FileStream stream = new FileStream(Path.Combine(outputFile, "Test"), FileMode.Create))
                {
                    sstream.CopyTo(stream);
                }
            }

            return response;
        }
    }

    public class Authentication
    {
        private const string SubscriptionKeyHeaderName = "Ocp-Apim-Subscription-Key";

        private string subscriptionKey;
        private Uri ibizaStsUrl;

        public Authentication(Uri ibizaStsUrl, string subscriptionKey)
        {
            this.ibizaStsUrl = ibizaStsUrl;
            this.subscriptionKey = subscriptionKey;
        }

        public string RetrieveNewTokenAsync()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(SubscriptionKeyHeaderName, this.subscriptionKey);
                var response = client.PostAsync(this.ibizaStsUrl, new StringContent(string.Empty)).Result;

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    throw new WebException(response.ReasonPhrase);
                }
            }
        }
    }
}
