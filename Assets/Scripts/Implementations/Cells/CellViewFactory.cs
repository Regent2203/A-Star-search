using UnityEngine;
using Zenject;

namespace Core.Implementations.Cells
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

        public CellView Create(Vector3 position, Vector2Int index, Vector2 scaleFactor, Transform parent)
        {
            var cellView = _instantiator.InstantiatePrefabForComponent<CellView>(_cellPrefab, position, Quaternion.identity, parent);
            cellView.Init(index, scaleFactor);

            return cellView;
        }        
    }
}