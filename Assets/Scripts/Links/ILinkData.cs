namespace ThisProject.Links
{
    public interface ILinkData<TId>
    {
        public TId From { get; }
        public TId To { get; }
        public float Cost { get; }

        public void ChangeCost(float value);
    }
}