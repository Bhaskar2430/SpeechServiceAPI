using CleanArc.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Interfaces
{
    public interface ISpeechService
    {
        Task<Responsemessage<TextToSpeechResponse>> TextToSpeech(int UserId, string textSpeech, string phonetic);
    }

    public class TextToSpeechResponse
    {
        public List<TextToSpeechViewModel> TextToSpeechViewModel { get; set; }
    }
    public class Responsemessage<T> where T : class
    {
        public T Data { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class ResponseStatus
    {
        public ErrorCode Code { get; set; }
        public bool Status { get; set; }
    }

    public enum ErrorCode
    {
        [Description("Success")]
        Success = 200,
        [Description("BadRequest")]
        BadRequest = 400
    }

}
