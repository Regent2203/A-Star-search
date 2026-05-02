using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.Implementations.Cells.UI
{
    public class UICellsPalleteChoiceItem : MonoBehaviour
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