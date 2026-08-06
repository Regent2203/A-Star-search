using System.Collections.Generic;
using UnityEngine;

namespace EasyField.GridNeighbours
{
    public interface IGridNeighboursProvider<T>
    {
        public IReadOnlyList<T> GetNeighbours(Vector2Int index, T[,] gridItems);
    }
}