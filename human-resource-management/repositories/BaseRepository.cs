namespace human_resource_management.repositories;

public class BaseRepository
{
    public abstract class Repository<T> where T : class
    {
        public abstract void Add(T entity);
        public abstract void Update(T entity);
        public abstract void Delete(T entity);
        public abstract T GetById(int id);
        public abstract List<T> GetAll();
    }
}