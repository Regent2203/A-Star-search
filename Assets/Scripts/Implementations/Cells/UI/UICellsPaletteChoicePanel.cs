using UnityEngine;

namespace Core.Implementations.Cells.UI
{
    public class UICellsPaletteChoicePanel : MonoBehaviour
    {
        [SerializeField]
        private UICellsPaletteChoiceItem _lmbChoiceItem;
        [SerializeField]
        private UICellsPaletteChoiceItem _rmbChoiceItem;


        public void SetLMBChoice(CellType cellType)
        {
            _lmbChoiceItem.SetIcon(cellType.Sprite);
        }

        public void SetRMBChoice(CellType cellType)
        {
            _rmbChoiceItem.SetIcon(cellType.Sprite);
        }
    }
}