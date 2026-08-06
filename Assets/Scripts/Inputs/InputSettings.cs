using System;
using UnityEngine;

namespace EasyField.Inputs
{
    [Serializable]
    public class InputSettings
    {
        public KeyCode CreatingKey = KeyCode.LeftControl;
        public KeyCode LinkingKey = KeyCode.LeftAlt;
        public KeyCode MarkingKey = KeyCode.LeftShift;
    }
}
