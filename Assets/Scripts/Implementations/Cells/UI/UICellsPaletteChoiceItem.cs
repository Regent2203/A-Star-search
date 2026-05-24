using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ThisProject.Implementations.Cells.UI
{
    public class UICellsPaletteChoiceItem : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private TMP_Text _text;

        
        public void SetIcon(Sprite sprite)
        {
            _icon.sprite = sprite;
        }
    }
}