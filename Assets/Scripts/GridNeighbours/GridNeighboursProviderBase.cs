using System.Collections.Generic;
using UnityEngine;

namespace ThisProject.GridNeighbours
{
    public abstract class GridNeighboursProviderBase<T> : IGridNeighboursProvider<T>
    {
        protected void TryAddCell(List<T> list, T[,] gridItems, int i, int j)
        {
            if (gridItems.IsIndexWithinBounds(i, j))
                list.Add(gridItems[i, j]);
        }

        public abstract IReadOnlyList<T> GetNeighbours(Vector2Int index, T[,] gridItems);
    }
}