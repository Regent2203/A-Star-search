using System.Collections.Generic;
using UnityEngine;

namespace EasyField.GridNeighbours
{
    public abstract class GridNeighboursProviderBase<TNodeData> : IGridNeighboursProvider<TNodeData>
    {
        protected abstract int Capacity { get; }
        protected readonly List<TNodeData> _neighboursList;

        public GridNeighboursProviderBase()
        {
            _neighboursList = new List<TNodeData>(Capacity);
        }

        protected void TryAddNeighbour(List<TNodeData> list, TNodeData[,] gridItems, int i, int j)
        {
            if (gridItems.IsIndexWithinBounds(i, j))
                if (gridItems[i, j] != null)
                    list.Add(gridItems[i, j]);
        }

        public abstract IReadOnlyList<TNodeData> GetNeighbours(Vector2Int index, TNodeData[,] gridItems);
    }
}