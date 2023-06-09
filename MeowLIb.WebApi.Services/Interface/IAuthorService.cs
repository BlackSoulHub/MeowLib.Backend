using LanguageExt.Common;
using MeowLib.Domain.DbModels.AuthorEntity;
using MeowLib.Domain.Dto.Author;
using MeowLib.Domain.Exceptions;
using MeowLib.Domain.Exceptions.Services;
using MeowLib.Domain.Requests.Author;

namespace MeowLIb.WebApi.Services.Interface;

/// <summary>
/// Абстракция сервиса для работы с авторами.
/// </summary>
public interface IAuthorService
{
    /// <summary>
    /// Метод создаёт нового автора.
    /// </summary>
    /// <param name="name">Имя автора.</param>
    /// <returns>DTO-модель автора.</returns>
    /// <exception cref="ValidationException">Возникает в случае ошибки валидации данных.</exception>
    Task<Result<AuthorDto>> CreateAuthorAsync(string name);

    /// <summary>
    /// Метод получает всех авторов.
    /// </summary>
    /// <returns>DTO список авторов.</returns>
    Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync();

    /// <summary>
    /// Метод обновляет информацию об авторе.
    /// </summary>
    /// <param name="id">Id автора.</param>
    /// <param name="updateAuthorEntityModel">Данные для обновления.</param>
    /// <returns>Обновлённую модель данных.</returns>
    /// <exception cref="ValidationException">Возникает в случае, если введёные данные некорректны.</exception>
    /// <exception cref="ApiException">Возникает если автор не был найден.</exception>
    Task<Result<AuthorDto>> UpdateAuthorAsync(int id, UpdateAuthorEntityModel updateAuthorEntityModel);

    /// <summary>
    /// Метод удаляет автора.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>True - в случае удачного удаления, false - в случае если автор не найден.</returns>
    /// <exception cref="ApiException">Возникает в случае внутренней ошибки.</exception>
    Task<Result<bool>> DeleteAuthorAsync(int id);

    /// <summary>
    /// Метод получает автора по его Id.
    /// </summary>
    /// <param name="authorId">Id автора.</param>
    /// <returns>DTO-модель автора.</returns>
    Task<Result<AuthorDto>> GetAuthorByIdAsync(int authorId);

    /// <summary>
    /// Метод получает список авторов подходящих под поисковые параметры.
    /// </summary>
    /// <param name="searchParams">Параметры для поиска.</param>
    /// <returns>Список авторов подходящих под параметры поиска.</returns>
    /// <exception cref="SearchNotFoundException">Возникает если не был найден автор по заданным параметрам поиска.</exception>
    Task<Result<IEnumerable<AuthorDto>>> GetAuthorWithParams(GetAuthorWithParamsRequest searchParams);
}