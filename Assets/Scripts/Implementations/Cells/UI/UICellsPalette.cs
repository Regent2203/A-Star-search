using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core.Implementations.Cells.UI
{
    public class UICellsPalette : MonoBehaviour
    {
        [SerializeField]
        private UICellsPaletteItem[] _allItems;

        [SerializeField]
        private UICellsPaletteChoiceItem _lmbChoiceItem;
        [SerializeField]
        private UICellsPaletteChoiceItem _rmbChoiceItem;


        private CellsPainter _cellsPainter;


        [Inject]
        public void Construct(CellsPainter cellsPainter)
        {
            _cellsPainter = cellsPainter;
        }

        private void Start()
        {
            foreach (var item in _allItems)
            {
                item.ItemClicked += OnPaletteItemClicked;
            }
        }

        private void OnDestroy()
        {
            foreach (var item in _allItems)
            {
                item.ItemClicked -= OnPaletteItemClicked;
            }
        }

        private void OnPaletteItemClicked(CellType type, PointerEventData.InputButton btn)
        {
            if (btn == PointerEventData.InputButton.Left) //lmb
            {
                _cellsPainter.SetLMBType(type);
                _lmbChoiceItem.SetIcon(type.Sprite);
            }
            else if (btn == PointerEventData.InputButton.Right) //rmb
            {
                _cellsPainter.SetRMBType(type);
                _rmbChoiceItem.SetIcon(type.Sprite);
            }
        }
    }
}