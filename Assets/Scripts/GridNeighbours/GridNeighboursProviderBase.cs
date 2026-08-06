using System.Collections.Generic;
using UnityEngine;

namespace EasyField.GridNeighbours
{
    public abstract class GridNeighboursProviderBase<TNodeData> : IGridNeighboursProvider<TNodeData>
    {
        protected void TryAddCell(List<TNodeData> list, TNodeData[,] gridItems, int i, int j)
        {
            if (gridItems.IsIndexWithinBounds(i, j))
                list.Add(gridItems[i, j]);
        }

        public abstract IReadOnlyList<TNodeData> GetNeighbours(Vector2Int index, TNodeData[,] gridItems);
    }
}