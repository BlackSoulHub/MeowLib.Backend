using MeowLib.Domain.DbModels.AuthorEntity;
using MeowLib.Domain.Dto.Author;

namespace MeowLib.WebApi.DAL.Repository.Interfaces;

public interface IAuthorRepository
{
    /// <summary>
    /// Метод создаёт нового автора.
    /// </summary>
    /// <param name="createAuthorData">Данный для создания автора.</param>
    /// <returns>DTO-модель автора</returns>
    Task<AuthorDto> CreateAsync(CreateAuthorEntityModel createAuthorData);
    
    /// <summary>
    /// Метод получает подробную информацию об авторе по его Id.
    /// </summary>
    /// <param name="id">Id автора</param>
    /// <returns>DTO-модель автора</returns>
    Task<AuthorDto?> GetByIdAsync(int id);
    
    /// <summary>
    /// Метод удаляет автора по Id.
    /// </summary>
    /// <param name="id">Id автора.</param>
    /// <returns>True - в случае удачного удаления, иначе - False. </returns>
    Task<bool> DeleteByIdAsync(int id);

    /// <summary>
    /// Метод получает список всех авторов
    /// </summary>
    /// <returns>Список всех авторов</returns>
    Task<IEnumerable<AuthorDto>> GetAll();
}