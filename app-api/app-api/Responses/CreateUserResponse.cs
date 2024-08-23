using System;
using System.Text.Json.Serialization;

namespace app.Responses
{
    public class BaseResposne {
        // [JsonPr("errorList")]
        public Dictionary<string, string[]?>? ErrorList {get;set;}
    }
    public class CreateUserResponse : BaseResposne
    {
      
    }

}