namespace EasyField
{
    public readonly struct DualKey<TId>
    {
        public TId From { get; }
        public TId To { get; }


        public DualKey(TId fromId, TId toId)
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