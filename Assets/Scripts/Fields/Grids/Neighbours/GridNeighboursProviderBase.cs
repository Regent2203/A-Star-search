using System.Collections.Generic;
using UnityEngine;

namespace Core.Fields.Grids.Neighbours
{
    public abstract class GridNeighboursProviderBase<T> : IGridNeighboursProvider<T>
    {
        protected void TryAddCell(List<T> list, T[,] gridItems, int i, int j)
        {
            if (gridItems.IsWithinBounds(i, j))
                list.Add(gridItems[i, j]);
        }

        public abstract IReadOnlyList<T> GetNeighbours(Vector2Int index, T[,] gridItems);
    }
}