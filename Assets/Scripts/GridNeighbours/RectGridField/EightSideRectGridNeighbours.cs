using System.Collections.Generic;
using UnityEngine;

namespace EasyField.GridNeighbours
{
    public class EightSideRectGridNeighbours<TNodeData> : GridNeighboursProviderBase<TNodeData> //all eight directions
    {
        protected override int Capacity => 8;

        public override IReadOnlyList<TNodeData> GetNeighbours(Vector2Int index, TNodeData[,] gridItems)
        {
            _neighboursList.Clear();

            int i = index.x;
            int j = index.y;

            if (gridItems.IsIndexWithinBounds(i, j))
            {
                TryAddNeighbour(_neighboursList, gridItems, i, j + 1);
                TryAddNeighbour(_neighboursList, gridItems, i + 1, j + 1);
                TryAddNeighbour(_neighboursList, gridItems, i + 1, j);
                TryAddNeighbour(_neighboursList, gridItems, i + 1, j - 1);
                TryAddNeighbour(_neighboursList, gridItems, i, j - 1);
                TryAddNeighbour(_neighboursList, gridItems, i - 1, j - 1);
                TryAddNeighbour(_neighboursList, gridItems, i - 1, j);
                TryAddNeighbour(_neighboursList, gridItems, i - 1, j + 1);
            }

            return _neighboursList;
        }
    }
}