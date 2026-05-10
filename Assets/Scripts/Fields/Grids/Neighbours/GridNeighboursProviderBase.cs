using Core.Nodes;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Fields.Grids.Neighbours
{
    public abstract class GridNeighboursProviderBase<T> : IGridNeighboursProvider<T>
        where T : class, INode<Vector2Int>
    {
        protected void TryAddCell(List<T> list, T[,] grid, int i, int j)
        {
            if (grid.IsWithinBounds(i, j))
                list.Add(grid[i, j]);
        }

        public abstract IReadOnlyList<T> GetNeighbours(Vector2Int index, T[,] gridNodes);
    }
}