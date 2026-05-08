using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Core.MainMenu
{
    public class UIExit : MonoBehaviour //this class is as simple as possible
    {
        [SerializeField]
        private Button _btnExit;

        const string startScene = "Starting Scene";


        private void Start()
        {
            _btnExit.onClick.AddListener(() => LoadScene(startScene));
        }

        private void LoadScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }
    }
}