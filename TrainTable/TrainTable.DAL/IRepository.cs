using System.Linq;
using System.Threading.Tasks;

namespace TrainTable.DAL
{
    /// <summary>
    /// This interface sets a set of methods that the repository should implement.
    /// </summary>
    /// <typeparam name="T">The type of model that is the object representation of the table.</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Method for inserting a new object into the database.
        /// </summary>
        /// <param name="entity">The object to insert into the database.</param>
        /// <returns>ID of the created object.</returns>
        Task<T> Create(T entity);

        /// <summary>
        /// Method for reading an object from the database.
        /// </summary>
        /// <param name="id">The object id to read from the database.</param>
        /// <returns>The read object from the database.</returns>
        Task<T> ReadById(int id);

        /// <summary>
        /// Method for updating an object in the database.
        /// </summary>
        /// <param name="entity">The object to update in the database.</param>
        Task<T> Update(T entity);

        /// <summary>
        /// Method for removing an object from the database.
        /// </summary>
        /// <param name="id">The object id to delete from the database.</param>
        Task Delete(int id);

        /// <summary>
        /// Method for reading all objects from the database.
        /// </summary>
        /// <returns>The list of objects read from the database.</returns>
        IQueryable<T> ReadAll();
    }
}
