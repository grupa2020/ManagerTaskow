using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProjectManager.Connection.Responses
{
    public class ValueResponse<T> : SimpleResponse
    {
        [JsonConstructor]
        public ValueResponse()
        {
        }

        public ValueResponse(bool succeeded, string message, T value) : base(succeeded, message)
        {
            Value = value;
        }

        [JsonProperty(PropertyName = "value")]
        public T Value { get; set; }
    }
}
