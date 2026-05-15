using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.Implementations.Cells.UI
{
    public class UICellsPaletteItem : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private TMP_Text _hotkeyText;

        private readonly string _textFormat = "{0}\n({1})\n[{2}]";

        private CellType _cellType;
        private Action<CellType, PointerEventData.InputButton> _clickCallback;
        public CellType CellType => _cellType;

        

        public void Init(CellType cellType, Action<CellType, PointerEventData.InputButton> clickCallback)
        {
            _cellType = cellType;
            _clickCallback = clickCallback;
            _icon.sprite = _cellType.Sprite;

            string moveCost = float.IsInfinity(_cellType.MoveCost) ? "∞" : _cellType.MoveCost.ToString("0.00", CultureInfo.InvariantCulture);
            _hotkeyText.text = string.Format(_textFormat, _cellType.Name, moveCost, _cellType.PaletteHotkey.ToString());
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _clickCallback?.Invoke(_cellType, eventData.button);
        }
    }
}