namespace MiniBank.Cache;

public interface IMinibankEntityCache<T>
{
    //IList<T> GetList(string key);
    bool SaveList(string key, IList<T> entities);
    //bool SaveEntity(T entity);
    List<T> GetList(string key);
}
