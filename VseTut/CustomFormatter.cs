using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VseTut.Web.Host.Models;

namespace VseTut.Web.Host
{
    public class CustomFormatter : OutputFormatter
    {
        public CustomFormatter()
        {
            this.SupportedMediaTypes.Clear();
            this.SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/json"));
        }

        public override bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            return base.CanWriteResult(context);
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            var response = context.HttpContext.Response;

            var result = new CustomResponse(context.Object, response.StatusCode);

            return Task.FromResult(response.WriteAsync(JsonConvert.SerializeObject(result)));
        }
    }
}
