namespace ThisProject.Links
{
    public interface ILinkView
    {
        public void UpdatePositions();
    }

    public interface ILinkView<TId> : ILinkView
    {
        public LinkKey<TId> Id { get; }
        public TId From { get; }
        public TId To { get; }
    }
}
