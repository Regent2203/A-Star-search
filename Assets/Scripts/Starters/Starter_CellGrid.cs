using System;
using UnityEngine;
using Core.SearchAlgorithms;
using Fields;
using Zenject;


namespace Core.Starters
{
    public class Starter_CellGrid : MonoBehaviour
    {
        private AbstractField _field;
        private ISearchAlgorithm _searchAlgorithm;


        [Inject]
        public void Construct(AbstractField field, ISearchAlgorithm searchAlgorithm)
        {
            _field = field;
            _searchAlgorithm = searchAlgorithm;
        }

        private void Start()
        {

            /*
            _field.ModeChangedCurrent += (currMode) =>
            {
                if (currMode == DrawMode.Launch)
                    RunAlgorithm();
            };*/
        }

        private void RunAlgorithm()
        {
            var path = _searchAlgorithm.GetPath(_field);

            //foreach (var node in path)
            //    Debug.Log(node);

            _field.ShowPath(true, path);
        }
    }
}