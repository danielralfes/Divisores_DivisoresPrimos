﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MathCalc.ApiCalc
{
    public class UnhandledExceptionLoggerFilter : IExceptionFilter
    {
        ILogger Log { get; } = ApplicationLogging.CreateLogger<UnhandledExceptionLoggerFilter>();

        public void OnException(ExceptionContext context)
        {
            var user = context.HttpContext?.User;
            var url = context.HttpContext?.Request.Path.ToString();
            var method = context.HttpContext?.Request.Method;

            //Descomentar o implementar Refit
            //if (context.Exception is Refit.ApiException apiEx)
            //{
                //log = log
                    //.ForContext("ApiResponseContent", apiEx.Content)
                    //.ForContext("ApiResponseStatusCode", apiEx.StatusCode);
            //}

            Log.LogError(context.Exception, "[{0}] Unhandled Error on Website API - [{1} {2}]", user, method, url);
        }
    }
}