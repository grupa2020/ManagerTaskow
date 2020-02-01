using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProjectManager.Connection.Responses
{
    public class SimpleResponse
    {
        [JsonConstructor]
        public SimpleResponse()
        {
            this.Message = "brak odpowiedzi";
            this.Succeeded = false;
        }

        public SimpleResponse(bool succeeded, string message)
        {
            Succeeded = succeeded;
            Message = message;
        }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "succeeded")]
        public bool Succeeded { get; set; }

    }
}
