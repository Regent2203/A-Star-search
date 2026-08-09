using UnityEngine;

namespace EasyField.Implementations.Cells
{
    public class CellViewFactory
    {
        private readonly CellViewPool _cellViewsPool;

        public CellViewFactory(CellViewPool cellViewsPool)
        {
            _cellViewsPool = cellViewsPool;
        }

        public CellView CreateItem(Vector2Int id, Vector2 pos)
        {
            var vertexView = _cellViewsPool.Spawn(id, pos);

            return vertexView;
        }

        public void DeleteItem(CellView item)
        {
            _cellViewsPool.Despawn(item);
        }
    }
}