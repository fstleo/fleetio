public interface IFactory<TOut, TIn>
{
    TOut Create(TIn param);
}

public interface IFactory<TOut>
{
    TOut Create();
}