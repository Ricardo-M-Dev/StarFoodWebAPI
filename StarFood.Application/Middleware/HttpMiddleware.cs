﻿using Microsoft.AspNetCore.Http;

namespace StarFood.Infrastructure.Middleware
{
    public class HttpMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke (HttpContext context)
        {
            var restaurantId = context.Request.Headers["X-RestaurantId"];

            if (string.IsNullOrEmpty(restaurantId))
            {
                context.Items["X-RestaurantId"] = null;
            }
            else
            {
                context.Items["X-RestaurantId"] = restaurantId;
            }

            return _next(context);
        }
    }
}
