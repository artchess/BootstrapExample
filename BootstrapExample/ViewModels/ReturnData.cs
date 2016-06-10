using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace BootstrapExample.ViewModels
{
    public class ReturnData
    {
        public ReturnData(HttpStatusCode httpStatusCode, string content, string reasonPhrase)
        {
            HttpStatusCode = httpStatusCode;
            Content = content;
            ReasonPhrase = reasonPhrase;
        }

        public HttpStatusCode HttpStatusCode { get; set; }
        public string Content { get; set; }
        public string ReasonPhrase { get; set; }
    }
}