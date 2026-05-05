using UnityEngine;
using Zenject;

namespace Core.Implementations.Cells
{
    public class CellsFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly Cell _cellPrefab;

        public CellsFactory(IInstantiator instantiator, Cell prefab)
        {
            _instantiator = instantiator;
            _cellPrefab = prefab;
        }

        public Cell Create(Vector3 position, Vector2Int index, Vector2 scaleFactor, Transform parent)
        {
            var cell = _instantiator.InstantiatePrefabForComponent<Cell>(_cellPrefab, position, Quaternion.identity, parent);
            cell.Init(index, scaleFactor);

            return cell;
        }        
    }
}