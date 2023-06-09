using LanguageExt.Common;
using MeowLib.Domain.Dto.User;
using MeowLib.Domain.Exceptions;
using MeowLib.Domain.Exceptions.Services;

namespace MeowLIb.WebApi.Services.Interface;

/// <summary>
/// Абстракция сервиса для работы с пользователями.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Метод создаёт нового пользователя.
    /// </summary>
    /// <param name="login">Логин пользователя</param>
    /// <param name="password">Пароль пользователя</param>
    /// <returns>Dto-модель пользователя.</returns>
    /// <exception cref="ApiException">Возникает в случае если логин пользователя занят.</exception>
    /// <exception cref="ValidationException">Возникает в случае ошибки валидации данных.</exception>
    Task<Result<UserDto>> SignInAsync(string login, string password);

    /// <summary>
    /// Метод создаёт JWT-токег для авторизации пользователя.
    /// </summary>
    /// <param name="login">Логин пользователя.</param>
    /// <param name="password">Пароль пользователя.</param>
    /// <returns>JWT-токен для авторизации.</returns>
    /// <exception cref="ApiException">Возникает в случае если указан неверный логин или пароль</exception>
    Task<Result<string>> LogIn(string login, string password);
}