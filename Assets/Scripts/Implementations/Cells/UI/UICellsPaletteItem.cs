using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.Implementations.Cells.UI
{
    public class UICellsPaletteItem : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField]
        private CellType _cellTypeItem;
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private TMP_Text _hotkeyText;

        public event Action<CellType, PointerEventData.InputButton> ItemClicked;

        
        private void Start()
        {
            _icon.sprite = _cellTypeItem.Sprite;
            _hotkeyText.text = _cellTypeItem.PaletteHotkey.ToString();
        }

        private void Update()
        {
            if (Input.GetKeyDown(_cellTypeItem.PaletteHotkey))
            {
                ItemClicked?.Invoke(_cellTypeItem, PointerEventData.InputButton.Left);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ItemClicked?.Invoke(_cellTypeItem, eventData.button);
        }
    }
}