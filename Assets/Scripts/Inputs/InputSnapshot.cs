namespace ThisProject.Inputs
{
    public readonly struct InputSnapshot
    {
        public bool IsMarkingMode { get; }
        public bool IsCreatingMode { get; }
        public bool IsLinkingMode { get; }

        public InputSnapshot(bool isMarking, bool isCreating, bool isLinking)
        {
            IsMarkingMode = isMarking;
            IsCreatingMode = isCreating;
            IsLinkingMode = isLinking;
        }
    }
}
