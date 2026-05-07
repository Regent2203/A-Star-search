using UnityEngine;
using Zenject;

namespace Core.Implementations.Cells
{
    public class CellViewFactory
    {
        private Vector2 _scaleFactor;
        private Transform _container;

        private readonly IInstantiator _instantiator;
        private readonly CellView _cellPrefab;


        public CellViewFactory(IInstantiator instantiator, CellView prefab)
        {
            _instantiator = instantiator;
            _cellPrefab = prefab;
        }

        public void SetConfiguration(Vector2 scaleFactor, Transform container)
        {
            _scaleFactor = scaleFactor;
            _container = container;
        }

        public CellView Create(Vector3 position, Vector2Int index)
        {
            var cellView = _instantiator.InstantiatePrefabForComponent<CellView>(_cellPrefab, position, Quaternion.identity, _container);
            cellView.Init(index, _scaleFactor);

            return cellView;
        }        
    }
}