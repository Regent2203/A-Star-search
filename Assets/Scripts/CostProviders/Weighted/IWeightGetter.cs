namespace Core.CostProviders
{
    public interface IWeightGetter<T>
    {
        public float GetWeight(T source);
    }    
}