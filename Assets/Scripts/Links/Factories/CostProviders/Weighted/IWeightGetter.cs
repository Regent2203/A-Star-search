namespace ThisProject.Links.Factories.CostProviders
{
    public interface IWeightGetter<T>
    {
        public float GetWeight(T source);
    }    
}