#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ThisProject.MainMenu
{
    public class UISceneLoadButton : MonoBehaviour
    {
        [Header("Drag scene asset here")]
        [SerializeField]
        private LazyLoadReference<Object> _sceneToLoad;

        [Space]
        [SerializeField]
        private Button _btn;

        [HideInInspector]
        [SerializeField]
        private string _sceneName;


        private void Start()
        {
            _btn.onClick.AddListener(LoadScene);
        }

        private void LoadScene()
        {
            SceneManager.LoadScene(_sceneName);
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
                ClearAssetReference();
                return;
            }

            _sceneName = _sceneToLoad.asset.name;
        }

        private void Reset()
        {
            _btn = GetComponent<Button>();
            ClearAssetReference();
        }
        #endif

        private void ClearAssetReference()
        {
            _sceneToLoad = null;
            _sceneName = string.Empty;
        }
    }
}