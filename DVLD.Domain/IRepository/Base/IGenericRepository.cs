namespace DVLD.Domain.IRepository.Base
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        public Task<IEnumerable<TEntity>> GetAllAsync(string storedProcedure);
        public Task<TEntity?> GetAsync(string storedProcedure, string propertyName, int value);
        public Task<int> AddAsync(string storedProcedure, TEntity entity);
        public Task<bool> UpdateAsync(string storedProcedure, TEntity entity);
        public Task<bool> DeleteAsync(string storedProcedure, string propertyName, int value);
        public Task<bool> IsExist(string storedProcedure, string propertyName, string value);
    }
}
