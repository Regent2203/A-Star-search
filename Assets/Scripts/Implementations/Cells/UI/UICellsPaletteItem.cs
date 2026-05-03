using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

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

        private string _textFormat = "{0}\n({1})";
        private KeyCode _markingKeyCode = KeyCode.LeftShift;


        [Inject]
        public void Construct([Inject(Id = "MarkingKey")] KeyCode markingKeyCode)
        {
            _markingKeyCode = markingKeyCode;
        }

        private void Start()
        {
            _icon.sprite = _cellTypeItem.Sprite;
            _hotkeyText.text = string.Format(_textFormat, _cellTypeItem.Name, _cellTypeItem.PaletteHotkey.ToString());
        }

        private void Update()
        {
            if (Input.GetKeyDown(_cellTypeItem.PaletteHotkey))
            {
                if (Input.GetKey(_markingKeyCode))
                    ItemClicked?.Invoke(_cellTypeItem, PointerEventData.InputButton.Right);
                else
                    ItemClicked?.Invoke(_cellTypeItem, PointerEventData.InputButton.Left);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ItemClicked?.Invoke(_cellTypeItem, eventData.button);
        }
    }
}