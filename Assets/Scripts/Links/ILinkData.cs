namespace EasyField.Links
{
    public interface ILinkData
    {
        public float Cost { get; }
        public bool IsBlocked { get; }
        public void SetCost(float value);
        public bool TrySetBlocked(bool blocked);
    }

    public interface ILinkData<TId> : ILinkData
    {
        public DualKey<TId> Id { get; }
        public TId From { get; }
        public TId To { get; }
    }
}