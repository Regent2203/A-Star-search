using Core.Nodes;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Fields.Grids.Neighbours
{
    public interface IGridNeighboursProvider<T> where T : class, INode<Vector2Int>
    {
        public IReadOnlyList<T> GetNeighbours(Vector2Int index, T[,] gridNodes);
    }
}