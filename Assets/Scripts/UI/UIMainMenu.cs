using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Core.MainMenu
{
    public class UIMainMenu : MonoBehaviour //this class is as simple as possible
    {
        [SerializeField]
        private Button _btnScene1;
        [SerializeField]
        private Button _btnScene2;
        [SerializeField]
        private Button _btnScene3;

        const string scene1 = "CellGrid Example1";
        const string scene2 = "CellGrid Example2";
        const string scene3 = "NodeGraph Example1";


        private void Start()
        {
            _btnScene1.onClick.AddListener(() => LoadScene(scene1));
            _btnScene2.onClick.AddListener(() => LoadScene(scene2));
            _btnScene3.onClick.AddListener(() => LoadScene(scene3));
        }

        private void LoadScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }
    }
}