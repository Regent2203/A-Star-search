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
        public CellType CellType => _cellType;

        public event Action<CellType, PointerEventData.InputButton> ItemClicked;


        public void Init(CellType cellType)
        {
            _cellType = cellType;
            _icon.sprite = _cellType.Sprite;

            string moveCost = float.IsInfinity(_cellType.MoveCost) ? "∞" : _cellType.MoveCost.ToString("0.00", CultureInfo.InvariantCulture);

            _hotkeyText.text = string.Format(_textFormat, _cellType.Name, moveCost, _cellType.PaletteHotkey.ToString());
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ItemClicked?.Invoke(_cellType, eventData.button);
        }
    }
}