using UnityEngine;

namespace UI
{
    public class UICanvas : MonoBehaviour
    {
        [SerializeField]
        private UIModeSwitcher _uiModeSwitcher = default;

        public UIModeSwitcher UIModeSwitcher => _uiModeSwitcher;
    }
}