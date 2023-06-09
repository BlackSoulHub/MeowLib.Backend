using MeowLib.Domain.Enums;
using MeowLib.Domain.Responses;
using MeowLIb.WebApi.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace MeowLib.WebApi.Filters;

public class AuthorizationAttribute : Attribute, IAsyncAuthorizationFilter
{
    public UserRolesEnum[] RequiredRoles { get; set; } = Array.Empty<UserRolesEnum>();
    
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.IsHttps)
        {
            context.Result = new JsonResult(new BaseErrorResponse("Доступ к методам с авторизацией только по HTTPS"))
            {
                StatusCode = 401
            };
            return;
        }

        var authToken = context.HttpContext.Request.Headers["Authorization"];
        if (authToken.Equals(StringValues.Empty))
        {
            context.Result = new JsonResult(new BaseErrorResponse("Токен доступа не найден"))
            {
                StatusCode = 401
            };
            return;
        }

        var jwtTokenService = context.HttpContext.RequestServices.GetService<IJwtTokenService>();
        if (jwtTokenService is null)
        {
            context.Result = new JsonResult(new BaseErrorResponse("Ошибка сервера"))
            {
                StatusCode = 500
            };
            return;
        }
        
        var parsedTokenData = await jwtTokenService.ParseTokenAsync(authToken.ToString().Replace("Bearer ", string.Empty));
        if (parsedTokenData is null)
        {
            context.Result = new JsonResult(new BaseErrorResponse("Ошибка валидации токена"))
            {
                StatusCode = 401
            };
            return;
        }

        if (RequiredRoles.Any())
        {
            if (!RequiredRoles.Contains(parsedTokenData.Role))
            {
                context.Result = new JsonResult(new BaseErrorResponse("Нет доступа"))
                {
                    StatusCode = 403
                };
                return;
            }
        }
        
        context.HttpContext.Items.Add("UserData", parsedTokenData);
    }
}