using WarehouseAPI.Domain.Interfaces.Repositories;

namespace WarehouseAPI.Infra.Repositories
{
    /// <summary>
    /// Classe genérica que implementa um repositório básico para operações CRUD (Create, Read, Update, Delete) em um contexto de banco de dados.
    /// </summary>
    /// <typeparam name="TEntity">O tipo da entidade com a qual o repositório trabalha.</typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
        {
            #region Attributes

            private readonly AppDbContext _context;

            #endregion

            #region Constructor

            public Repository(AppDbContext context)
            {
                this._context = context;
            }

            #endregion

            #region Methods
            public TEntity Create(TEntity entity)
            {
                _context.Add(entity);
                return entity;
            }

            public void Delete(TEntity entity)
            {
                _context.Set<TEntity>().Remove(entity);
            }

            public TEntity FindById(int id)
            {
                return _context.Set<TEntity>().Find(id);
            }

            public IQueryable<TEntity> GetAll()
            {
                return _context.Set<TEntity>();
            }
            public void Update(TEntity entity)
            {
                _context.Set<TEntity>().Update(entity);
            }

            public void SaveChanges()
            {
                _context.SaveChanges();
            }

            #endregion
        }
    }