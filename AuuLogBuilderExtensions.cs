//Author: Nathan Li
//Create Time: Tuesday, 21 January 2020

using System;
using Microsoft.AspNetCore.Builder;

namespace auu.log
{
    public static class AuuLogBuilderExtensions
    {
        public static IApplicationBuilder UseAuuLog(
            this IApplicationBuilder app,
            Action<AuuLogOptions> setupAction = null)
        {
            if (setupAction == null)
            {
                app.UseMiddleware<AuuLogMiddleware>();
            }
            else
            {
                var auuLogOptions = new AuuLogOptions();
                setupAction(auuLogOptions);
                app.UseMiddleware<AuuLogMiddleware>((object) auuLogOptions);
            }

            return app;
        }
    }
}