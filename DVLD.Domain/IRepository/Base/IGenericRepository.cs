namespace DVLD.Domain.IRepository.Base
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        public Task<IEnumerable<TEntity>> GetAllAsync(string storedProcedure, object? paramObject = null);
        public Task<IEnumerable<View>> GetAllAsync<View>(string storedProcedure, object? paramObject = null);
        public Task<TEntity?> GetByIdAsync(string storedProcedure, string propertyName, int value);
        public Task<View?> GetByIdAsync<View>(string storedProcedure, string propertyName, int value);
        public Task<int> AddAsync(string storedProcedure, TEntity entity, object? IncluedPropertyInSqlPrameter = null);
        public Task<bool> UpdateAsync(string storedProcedure, TEntity entity);
        public Task<bool> DeleteAsync(string storedProcedure, string propertyName, int value);

    }
}
