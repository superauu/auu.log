using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace auu.log
{
    public class AuuLogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AuuLogOptions _options;

        public AuuLogMiddleware(RequestDelegate next, IOptions<AuuLogOptions> optionsAccessor) : this(next,
            optionsAccessor.Value)
        {
        }

        public AuuLogMiddleware(RequestDelegate next, AuuLogOptions options)
        {
            _next = next;
            _options = options;
        }

        public async Task Invoke(HttpContext context)
        {
            var method = context.Request.Method;
            var input = context.Request.Path.Value;

            if (method == "POST" && input == _options.LogPath)
            {
                var body = new StreamReader(context.Request.Body);
                body.BaseStream.Seek(0, SeekOrigin.Begin);
                var content = body.ReadToEnd();
                var log = JsonSerializer.Deserialize<LogEntity>(content);
                _options.DbLogger.SaveLog(log);
                await ResponseOk(context.Response);
            }
            else if (method == "GET" && Regex.IsMatch(input, "^/" + _options.LogPath + "/?$"))
            {
                var param = input.Split('/').Last();
                if (int.TryParse(param, out var last))
                {
                    var logs = _options.DbLogger.GetLogTitle(last);
                    await ResponseJson(context.Response, JsonSerializer.Serialize(logs));
                }
                else
                {
                    var log = _options.DbLogger.GetLog(param);
                    await ResponseJson(context.Response, JsonSerializer.Serialize(log));
                }
            }
            else
            {
                await _next.Invoke(context);
            }
        }

        public async Task ResponseOk(HttpResponse response)
        {
            response.StatusCode = 200;
            response.ContentType = "text/html";
            await response.WriteAsync("OK");
        }

        public async Task ResponseJson(HttpResponse response, string json)
        {
            response.StatusCode = 200;
            response.ContentType = "application/json";
            await response.WriteAsync(json);
        }
    }
}