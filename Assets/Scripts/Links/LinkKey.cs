namespace ThisProject.Links
{
    public readonly struct LinkKey<TId>
    {
        public TId From { get; }
        public TId To { get; }


        public LinkKey(TId fromId, TId toId)
        {
            From = fromId;
            To = toId;
        }

        public override string ToString()
        {
            return $"LinkKey({From}->{To})";
        }
    }
}
