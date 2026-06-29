using ThisProject.Nodes;
using UnityEngine;
using Zenject;

namespace ThisProject.Implementations.Cells
{
    public class CellNode : NodeData<Vector2Int>, IPoolable<Vector2Int, Vector2, CellType>
    {
        private CellType _cellType;

        public override bool IsBlocked => float.IsPositiveInfinity(_cellType.MoveCost) || _isBlocked;
        public CellType CellType => _cellType;


        public void OnSpawned(Vector2Int id, Vector2 nodePosition, CellType cellType)
        {
            base.OnSpawned(id, nodePosition);

            _cellType = cellType;
        }

        public override void OnDespawned()
        {
            _cellType = null;

            base.OnDespawned();
        }

        public bool TryChangeCellType(CellType cellType)
        {
            if (_cellType != cellType)
            {
                _cellType = cellType;
                return true;
            }
            return false;
        }
    }
}