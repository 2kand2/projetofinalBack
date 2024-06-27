using WarehouseAPI.Domain.Entities;

namespace WarehouseAPI.Domain.Interfaces.Repositories
{
    public interface ILocationRepository
    {
        /// <summary>
        /// Obtém uma lista de todos as localizações.
        /// </summary>
        /// <param name="companyId">Id da empresa</param>
        /// <returns>Um consulta (queryable) contendo todos os registros da entidade localização</returns>
        List<Location> getAll(int companyId);

        /// <summary>
        /// Obtém todos os os registros da entidade localização.
        /// </summary>
        /// <param name="id">Id da localização</param>
        /// <param name="companyId">Id da empresa</param>
        /// <returns></returns>
        Location FindById(int id, int companyId);

        /// <summary>
        /// Cria um novo registro da entidade localização no repositório.
        /// </summary>
        /// <param name="location">O objeto da entidade a ser criado.</param>
        void Create(Location location);

        /// <summary>
        /// Remove um registro da entidade Localização no respositório.
        /// </summary>
        /// <param name="location">O objeto da entidade a ser criado.</param>
        /// <param name="companyId">Id da empresa</param>
        void Delete(Location location, int companyId);

        /// <summary>
        /// Atualiza um registro da entidade localização no repositório.
        /// </summary>
        /// <param name="location">O objeto da entidade a ser criado.</param>
        /// <param name="companyId">Id da empresa</param>
        void Update(Location location, int companyId);

        /// <summary>
        /// Salva as alterações realizadas no contexto do banco de dados.
        /// </summary>
        void SaveChanges();
    }
}
