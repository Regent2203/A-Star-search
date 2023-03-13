using UnityEngine;
using UnityEngine.UI;
using Demo;

namespace UI
{
    public class UIModeSwitcher : MonoBehaviour
    {
        [SerializeField]
        private ToggleGroup _toggleGroup = default;
        [SerializeField]
        private Toggle _initToggle = default;

        private AbstractField _field;


        public void Init(AbstractField field)
        {
            _field = field;            
            
            _toggleGroup.SetAllTogglesOff();
            _initToggle.isOn = true;
            _toggleGroup.allowSwitchOff = false;
        }

        public void SwitchModeObstacles(bool value)
        {
            if (value)
                _field.SetMode(FieldMode.SelectObstacles);
        }

        public void SwitchModeStartAndFinish(bool value)
        {
            if (value)
                _field.SetMode(FieldMode.SelectStartFinish);
        }

        public void SwitchModeLaunch(bool value)
        {
            if (value)
                _field.SetMode(FieldMode.Launch);
        }
    }
}