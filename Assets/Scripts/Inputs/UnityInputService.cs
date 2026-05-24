using UnityEngine;

namespace ThisProject.Inputs
{
    public class UnityInputService : IInputService
    {
        private readonly InputSettings _settings;

        public UnityInputService(InputSettings settings)
        {
            _settings = settings;
        }
        
        public bool IsMarkingMode => Input.GetKey(_settings.MarkingKey);
        public bool IsCreatingMode => Input.GetKey(_settings.CreatingKey);
        public bool IsLinkingMode => Input.GetKey(_settings.LinkingKey);

        public InputSnapshot CreateSnapshot()
        {
            return new InputSnapshot(IsMarkingMode, IsCreatingMode, IsLinkingMode);
        }
    }
}