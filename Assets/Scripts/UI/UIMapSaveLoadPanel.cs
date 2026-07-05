using ThisProject.Savers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ThisProject.UICommon
{
    public class UIMapSaveLoadPanel : MonoBehaviour
    {
        [SerializeField]
        private Button _btnSave;
        [SerializeField]
        private Button _btnLoad;

        private ISaver _saver;


        [Inject]
        public void Construct(ISaver saver)
        {
            _saver = saver;
        }

        private void Start()
        {
            _btnSave.onClick.AddListener(Save);
            _btnLoad.onClick.AddListener(Load);
        }

        private void Save()
        {
            _saver.SaveAsync();
        }

        private void Load()
        {
            _saver.Load();
        }
    }
}
