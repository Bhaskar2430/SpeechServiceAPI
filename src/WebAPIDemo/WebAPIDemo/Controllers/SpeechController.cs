using CleanArc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CleanArc.WebAPI.Controllers
{
    using Microsoft.AspNetCore.Cors;
    using System.Threading.Tasks;   
    [Route("api/")]
    [ApiController]
    [EnableCors("AllowAnyOrigin")]
    public class SpeechController : ControllerBase
    {

        private readonly ILogger<SpeechController> _logger;
        private readonly ISpeechService _speechService;

        public SpeechController(ILogger<SpeechController> logger, ISpeechService speechService)
        {
            _logger = logger;
            _speechService = speechService;
        }
        [HttpGet("TextToSpeech")]
        [Produces("application/json")]
        [EnableCors("AllowAnyOrigin")]
        public async Task<ActionResult> TextToSpeech(int UserId, string textSpeech, string phonetic)
        {
            Responsemessage<TextToSpeechResponse> res = new Responsemessage<TextToSpeechResponse>();

            if (UserId <= 0)
            {
                return BadRequest("UserId Should not be less than or equal to zero");
            }

            res = await _speechService.TextToSpeech(UserId, textSpeech, phonetic);
            return Ok(res);
        }

        [HttpPost("TextToSpeech")]
        [Produces("application/json")]
        [EnableCors("AllowAnyOrigin")]
        public async Task<ActionResult> TextToSpeech([FromBody] SpeechRequet speechRequet)
        {
            Responsemessage<TextToSpeechResponse> res = new Responsemessage<TextToSpeechResponse>();

            if (speechRequet.UserId <= 0)
            {
                return BadRequest("UserId Should not be less than or equal to zero");
            }

            res = await _speechService.TextToSpeech(speechRequet.UserId, speechRequet.textSpeech, speechRequet.phonetic);
            return Ok(res);
        }
        [EnableCors("AllowAnyOrigin")]
        public class SpeechRequet
        {
            public int UserId { get; set; }
            public string textSpeech { get; set; }
            public string phonetic { get; set; }


        }
    }





}
