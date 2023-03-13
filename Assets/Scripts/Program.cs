using System;
using UnityEngine;
using Algorithm;
using UI;

namespace Demo
{
    public class Program : MonoBehaviour
    {
        [SerializeField]
        private AbstractField _field = default;
        [SerializeField]
        private UICanvas _ui = default;


        private void Start()
        {
            _field.Initialize();
            _ui.UIModeSwitcher.Init(_field);
        }
    }
}
