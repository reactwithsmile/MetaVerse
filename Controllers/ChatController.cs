using Google.Cloud.Dialogflow.V2;
using Microsoft.AspNetCore.Mvc;

namespace MetaSphere.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly string _projectId;

        public ChatController(IConfiguration configuration)
        {
            _projectId = configuration["Dialogflow:ProjectId"] ?? global::System.Environment.GetEnvironmentVariable("DIALOGFLOW_PROJECT_ID");
        }

        public class ChatRequest
        {
            public string Text { get; set; } = string.Empty;
            public string? SessionId { get; set; }
        }

        public class ChatResponse
        {
            public string SessionId { get; set; }
            public string Reply { get; set; }

            public ChatResponse(string sessionId, string reply)
            {
                SessionId = sessionId;
                Reply = reply;
            }
        }

        [HttpPost("message")]
        public async Task<IActionResult> PostMessage([FromBody] ChatRequest req)
        {
            if (req == null || string.IsNullOrWhiteSpace(req.Text))
            {
                return BadRequest("Missing text");
            }

            if (string.IsNullOrEmpty(_projectId))
            {
                return BadRequest("Dialogflow project id not configured. Set Dialogflow:ProjectId or DIALOGFLOW_PROJECT_ID environment variable.");
            }

            var sessionId = string.IsNullOrEmpty(req.SessionId) ? Guid.NewGuid().ToString() : req.SessionId;
            var sessionName = SessionName.FromProjectSession(_projectId, sessionId);

            SessionsClient client = await SessionsClient.CreateAsync();

            var queryInput = new QueryInput
            {
                Text = new TextInput
                {
                    Text = req.Text,
                    LanguageCode = "en-US"
                }
            };

            var response = await client.DetectIntentAsync(sessionName, queryInput);
            var fulfillment = response?.QueryResult?.FulfillmentText ?? string.Empty;

            return Ok(new ChatResponse(sessionId, fulfillment));
        }
    }
}
