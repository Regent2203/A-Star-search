using UnityEngine;

namespace EasyField.Implementations.Cells
{
    public class CellDataFactory
    {
        private readonly CellDataPool _cellDatasPool;

        public CellDataFactory(CellDataPool cellDatasPool)
        {
            _cellDatasPool = cellDatasPool;
        }

        public CellData CreateItem(Vector2Int id, Vector2 nodePos, CellType cellType)
        {
            var vertexData = _cellDatasPool.Spawn(id, nodePos, cellType);

            return vertexData;
        }

        public void DeleteItem(CellData item)
        {
            _cellDatasPool.Despawn(item);
        }
    }
}