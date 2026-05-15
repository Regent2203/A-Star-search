namespace Core.Inputs
{
    public interface IInputService
    {
        public bool IsMarkingMode { get; }
        public bool IsCreatingMode { get; }
        public bool IsLinkingMode { get; }

        public InputSnapshot CreateSnapshot();
    }
}