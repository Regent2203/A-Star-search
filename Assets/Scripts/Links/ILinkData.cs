namespace ThisProject.Links
{
    public interface ILinkData
    {        
        public float Cost { get; }
        public void ChangeCost(float value);
    }

    public interface ILinkData<TId> : ILinkData
    {
        public LinkKey<TId> Id { get; }
        public TId From { get; }
        public TId To { get; }
    }
}