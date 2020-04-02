using System;

/// <summary>
/// unsafe but fast
/// </summary>
/// <typeparam name="T"></typeparam>
public class SimpleObjectPool<T> : Pool<T>
{
    readonly Action<T> mResetMethod;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="factoryMethod">创建</param>
    /// <param name="resetMethod">回收</param>
    /// <param name="initCount"></param>
    public SimpleObjectPool(Func<T> factoryMethod, Action<T> resetMethod = null, int initCount = 0)
    {
        mFactory = new CustomObjectFactory<T>(factoryMethod);
        mResetMethod = resetMethod;

        for (int i = 0; i < initCount; i++)
        {
            mCacheStack.Push(mFactory.Create());
        }
    }

    public override bool Recycle(T obj)
    {
        mResetMethod.Invoke(obj);
        mCacheStack.Push(obj);
        return true;
    }
}