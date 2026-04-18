using System;
using UnityEngine;
using Algorithm;
using UI;
using Fields;
using Core;


namespace Core.Starters
{
    public class Starter : MonoBehaviour
    {
        [SerializeField]
        private AbstractField _field;
        [SerializeField]
        private UICanvas _ui;


        private void Start()
        {
            _field.Initialize();
            _ui.UIModeSwitcher.Init(_field);

            _field.ModeChangedCurrent += (currMode) =>
            {
                if (currMode == DrawMode.Launch)
                    RunAlgorithm();
            };
        }

        private void Variant1()
        {
            //A-star algorithm
            //CellView-Grid
            //UI
        }

        private void RunAlgorithm()
        {
            var alg = new AStarSearchAlgorithm(_field);
            var path = alg.CalculateWay();

            //foreach (var node in path)
            //    Debug.Log(node);

            _field.ShowPath(true, path);
        }
    }
}