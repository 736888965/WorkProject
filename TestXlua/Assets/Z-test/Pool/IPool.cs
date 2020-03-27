
public interface IPool<T>
{
    T Allocate();
    bool Recycle(T obj);
}

public interface IObjectFactory<T>
{
    T Create();
}




public class DefaultObjectFactory<T> : IObjectFactory<T> where T : new()
{
    public T Create()
    {
        return new T();
    }
}
