using System;
using UnityEngine;
using Algorithm;
using UI;
using Fields;


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

        _field.ModeChangedCurrent += (currMode) =>
        {
            if (currMode == FieldMode.Launch)
                RunAlgorithm();
        };
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

