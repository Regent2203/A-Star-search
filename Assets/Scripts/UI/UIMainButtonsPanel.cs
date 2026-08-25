using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EasyField.UICommon
{
    public class UIMainButtonsPanel : MonoBehaviour
    {
        [SerializeField]
        private Button _btnSave;
        [SerializeField]
        private Button _btnLoad;
        
        [Space]
        [SerializeField]
        private Button _btnNew;
        [SerializeField]
        private TMP_InputField _inputSizeX;
        [SerializeField]
        private TMP_InputField _inputSizeY;

        public event Action SaveBtnClicked;
        public event Action LoadBtnClicked;
        public event Action<int, int> NewBtnClicked;


        private void Start()
        {
            _btnSave.onClick.AddListener(Save);
            _btnLoad.onClick.AddListener(Load);
            _btnNew.onClick.AddListener(New);
        }

        private void Save()
        {
            SaveBtnClicked?.Invoke();
        }

        private void Load()
        {
            LoadBtnClicked?.Invoke();
        }

        private void New()
        {
            if (int.TryParse(_inputSizeX.text, out int X) && int.TryParse(_inputSizeY.text, out int Y))
                NewBtnClicked?.Invoke(X,Y);
        }

        public void SetInputValues(int x, int y)
        {
            _inputSizeX.text = x.ToString();
            _inputSizeY.text = y.ToString();
        }
    }
}
