using AutoMapper;
using MeowLib.Domain.DbModels.AuthorEntity;
using MeowLib.Domain.Dto.Author;
using MeowLib.WebApi.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeowLib.WebApi.DAL.Repository.Implementation.Production;

public class AuthorRepository : IAuthorRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;
    
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="applicationDbContext">Контекст база данных.</param>
    /// <param name="mapper">Автомаппер.</param>
    public AuthorRepository(ApplicationDbContext applicationDbContext, IMapper mapper)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    /// <summary>
    /// Метод создаёт нового автора.
    /// </summary>
    /// <param name="createAuthorData">Данный для создания автора.</param>
    /// <returns>DTO-модель автора</returns>
    public async Task<AuthorDto> CreateAsync(CreateAuthorEntityModel createAuthorData)
    {
        var newAuthor = _mapper.Map<CreateAuthorEntityModel, AuthorEntityModel>(createAuthorData);
        var dbResult = await _applicationDbContext.Authors.AddAsync(newAuthor);
        
        await _applicationDbContext.SaveChangesAsync();
        
        var authorDto = _mapper.Map<AuthorEntityModel, AuthorDto>(dbResult.Entity);
        return authorDto;
    }

    /// <summary>
    /// Метод получает подробную информацию об авторе по его Id.
    /// </summary>
    /// <param name="id">Id автора</param>
    /// <returns>DTO-модель автора</returns>
    public async Task<AuthorDto?> GetByIdAsync(int id)
    {
        var foundedAuthor = await GetAuthorById(id);
        if (foundedAuthor is null)
        {
            return null;
        }

        var authorDto = _mapper.Map<AuthorEntityModel, AuthorDto>(foundedAuthor);
        return authorDto;
    }

    /// <summary>
    /// Метод удаляет автора по Id.
    /// </summary>
    /// <param name="id">Id автора.</param>
    /// <returns>True - в случае удачного удаления, иначе - False. </returns>
    public async Task<bool> DeleteByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Метод получает список всех авторов
    /// </summary>
    /// <returns>Список всех авторов</returns>
    public async Task<IEnumerable<AuthorDto>> GetAll()
    {
        var authors = await _applicationDbContext.Authors.ToListAsync();

        return _mapper.Map<IEnumerable<AuthorEntityModel>, IEnumerable<AuthorDto>>(authors);
    }

    /// <summary>
    /// Метод получает автора по его Id.  
    /// </summary>
    /// <param name="id">Id автора.</param>
    /// <returns>Модель автора если он был найден, иначе - null</returns>
    private async Task<AuthorEntityModel?> GetAuthorById(int id)
    {
        var foundedAuthor = await _applicationDbContext.Authors.FirstOrDefaultAsync(a => a.Id == id);

        return foundedAuthor;
    }
}