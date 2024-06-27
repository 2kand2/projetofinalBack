using System.Collections.Generic;
using System.Threading.Tasks;
using WarehouseAPI.Domain.Entities;

namespace WarehouseAPI.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Interface que define operações disponíveis para manipulação de entidades AssetType.
    /// </summary>
    public interface IAssetTypeRepository
    {
        /// <summary>
        /// Obtém todos os tipos de ativos.
        /// </summary>
        /// <returns>Lista de todos os tipos de ativos.</returns>
        Task<List<AssetType>> GetAllAsync();

        /// <summary>
        /// Busca um tipo de ativo pelo seu ID.
        /// </summary>
        /// <param name="id">ID do tipo de ativo a ser buscado.</param>
        /// <returns>O tipo de ativo encontrado ou null se não existir.</returns>
        Task<AssetType> FindByIdAsync(int id);

        /// <summary>
        /// Busca um tipo de ativo pelo seu nome.
        /// </summary>
        /// <param name="name">Nome do tipo de ativo a ser buscado.</param>
        /// <returns>O tipo de ativo encontrado ou null se não existir.</returns>
        Task<AssetType> FindByNameAsync(string name);

        /// <summary>
        /// Cria um novo tipo de ativo no banco de dados.
        /// </summary>
        /// <param name="assetType">Objeto AssetType a ser criado.</param>
        Task CreateAsync(AssetType assetType);

        /// <summary>
        /// Remove um tipo de ativo do banco de dados.
        /// </summary>
        /// <param name="assetType">Objeto AssetType a ser removido.</param>
        void Delete(AssetType assetType);

        /// <summary>
        /// Atualiza um tipo de ativo no banco de dados.
        /// </summary>
        /// <param name="assetType">Objeto AssetType a ser atualizado.</param>
        void Update(AssetType assetType);

        /// <summary>
        /// Salva todas as mudanças feitas no contexto do banco de dados.
        /// </summary>
        /// <returns>Uma tarefa assíncrona representando a operação de salvamento.</returns>
        Task SaveChangesAsync();
    }
}
