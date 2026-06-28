using UnityEngine;
using Zenject;

namespace ThisProject.Implementations.Cells
{
    public class CellViewFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly CellView _cellPrefab;


        public CellViewFactory(IInstantiator instantiator, CellView prefab)
        {
            _instantiator = instantiator;
            _cellPrefab = prefab;
        }

        public CellView Create(Vector2Int index, Vector3 position, Vector2 scaleFactor, Transform container)
        {
            var cellView = _instantiator.InstantiatePrefabForComponent<CellView>(_cellPrefab, position, Quaternion.identity, container);
            cellView.Init(index, scaleFactor);

            return cellView;
        }        
    }
}