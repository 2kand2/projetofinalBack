namespace WarehouseAPI.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Obtém todos os registros da entidade.
        /// </summary>
        /// <returns>Uma consulta (queryable) contendo todos os registros da entidade.</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Obtém os registros da entidade pelo seu id.
        /// </summary>
        /// <returns>Um registro da entidade</returns>
        TEntity FindById(int id);

        /// <summary>
        /// Cria um novo registro da entidade no repositório.
        /// </summary>
        /// <param name="entity">A entidade a ser criada.</param>
        /// <returns>O registro recém-criado da entidade.</returns>
        TEntity Create(TEntity entity);

        /// <summary>
        /// Remove um registro da entidade do repositório.
        /// </summary>
        /// <param name="entity">A entidade a ser removida.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Atualiza um registro da entidade no repositório.
        /// </summary>
        /// <param name="entity">A entidade a ser atualizada.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Salva as alterações realizadas no contexto do banco de dados.
        /// </summary>
        void SaveChanges();

    }
}
