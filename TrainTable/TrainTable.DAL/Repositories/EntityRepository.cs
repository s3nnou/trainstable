using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TrainTable.DAL.Repositories
{
    /// <summary>
    /// The class implements the repository pattern using ef core.
    /// </summary>
    /// <typeparam name="T">The type of model that is the object representation of the table.</typeparam>
    internal class EntityRepository<T> : IRepository<T>
        where T : class
    {
        private readonly DbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityRepository{T}"/> class.
        /// </summary>
        /// <param name="dbContext">Data base context.</param>
        public EntityRepository(TrainTableDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc cref="IRepository{T}.Create(T)"/>
        public async Task<T> Create(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        /// <inheritdoc cref="IRepository{T}.Delete(int)"/>
        public async Task Delete(int id)
        {
            T entity = await _dbContext.Set<T>().FindAsync(id);

            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc cref="IRepository{T}.ReadAll"/>
        public IQueryable<T> ReadAll()
        {
            var result = _dbContext.Set<T>().AsNoTracking();

            return result;
        }

        /// <inheritdoc cref="IRepository{T}.ReadById(int)"/>
        public async Task<T> ReadById(int id)
        {
            var result = await _dbContext.Set<T>().FindAsync(id);

            return result;
        }

        /// <inheritdoc cref="IRepository{T}.Update(T)"/>
        public async Task<T> Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}
