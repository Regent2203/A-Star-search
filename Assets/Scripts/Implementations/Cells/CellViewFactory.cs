using UnityEngine;
using Zenject;

namespace ThisProject.Implementations.Cells
{
    public class CellViewFactory
    {
        private Transform _container;

        private readonly IInstantiator _instantiator;
        private readonly CellView _cellPrefab;


        public CellViewFactory(IInstantiator instantiator, CellView prefab)
        {
            _instantiator = instantiator;
            _cellPrefab = prefab;
        }

        public void SetConfiguration(Transform container)
        {
            _container = container;
        }

        public CellView Create(Vector2Int index, Vector3 position, Vector2 scaleFactor)
        {
            var cellView = _instantiator.InstantiatePrefabForComponent<CellView>(_cellPrefab, position, Quaternion.identity, _container);
            cellView.Init(index, scaleFactor);

            return cellView;
        }        
    }
}