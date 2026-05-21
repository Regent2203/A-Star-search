using System.Collections.Generic;
using UnityEngine;

namespace Core.Fields.Grids.Neighbours
{
    public interface IGridNeighboursProvider<T>
    {
        public IReadOnlyList<T> GetNeighbours(Vector2Int index, T[,] gridItems);
    }
}