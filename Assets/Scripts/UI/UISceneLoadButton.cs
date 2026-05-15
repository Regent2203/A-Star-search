#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Core.MainMenu
{
    public class UISceneLoadButton : MonoBehaviour
    {
        [Header("Drag scene asset here")]
        [SerializeField]
        private LazyLoadReference<Object> _sceneToLoad;

        [Space]
        [SerializeField]
        private Button _btn;


        private void Start()
        {
            _btn.onClick.AddListener(() => LoadScene(_sceneToLoad.asset.name));
        }

        private void LoadScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }

        #if UNITY_EDITOR
        private void OnValidate()
        {
            if (!_sceneToLoad.isSet)
            {
                return;
            }
            
            if (_sceneToLoad.asset.GetType() != typeof(SceneAsset))
            {
                Debug.LogError($"You can only assign Scene to the field '{nameof(_sceneToLoad)}'", this);
                _sceneToLoad = null;
            }
        }

        private void Reset()
        {
            _sceneToLoad.asset = null;
            _btn = GetComponent<Button>();
        }
        #endif
    }
}