namespace CQRSPattern.Common.Builder.Collection;

public abstract class CollectionBuilder<TCollection>
{
    protected TCollection Collection;

    protected abstract void Reset();

    public TCollection Build()
    {
        var collection = this.Collection;
        Reset();

        return collection;
    }
}