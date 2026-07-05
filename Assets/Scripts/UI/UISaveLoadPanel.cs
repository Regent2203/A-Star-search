using System;
using UnityEngine;
using UnityEngine.UI;

namespace ThisProject.UICommon
{
    public class UISaveLoadPanel : MonoBehaviour
    {
        [SerializeField]
        private Button _btnSave;
        [SerializeField]
        private Button _btnLoad;

        public event Action SaveBtnClicked;
        public event Action LoadBtnClicked;


        private void Start()
        {
            _btnSave.onClick.AddListener(Save);
            _btnLoad.onClick.AddListener(Load);
        }

        private void Save()
        {
            SaveBtnClicked?.Invoke();
        }

        private void Load()
        {
            LoadBtnClicked?.Invoke();
        }
    }
}
