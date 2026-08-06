using System.Collections.Generic;
using UnityEngine;

namespace EasyField.GridNeighbours
{
    public interface IGridNeighboursProvider<TNodeData>
    {
        public IReadOnlyList<TNodeData> GetNeighbours(Vector2Int index, TNodeData[,] gridItems);
    }
}