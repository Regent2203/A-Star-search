using System.Collections.Generic;
using UnityEngine;

namespace EasyField.GridNeighbours
{
    public class EightSideGridNeighbours<TNodeData> : GridNeighboursProviderBase<TNodeData> //all eight directions
    {
        private readonly List<TNodeData> _neighboursList = new List<TNodeData>(8);

        public override IReadOnlyList<TNodeData> GetNeighbours(Vector2Int index, TNodeData[,] gridItems)
        {
            _neighboursList.Clear();

            int i = index.x;
            int j = index.y;

            if (gridItems.IsIndexWithinBounds(i, j))
            {
                TryAddCell(_neighboursList, gridItems, i, j + 1);
                TryAddCell(_neighboursList, gridItems, i + 1, j + 1);
                TryAddCell(_neighboursList, gridItems, i + 1, j);
                TryAddCell(_neighboursList, gridItems, i + 1, j - 1);
                TryAddCell(_neighboursList, gridItems, i, j - 1);
                TryAddCell(_neighboursList, gridItems, i - 1, j - 1);
                TryAddCell(_neighboursList, gridItems, i - 1, j);
                TryAddCell(_neighboursList, gridItems, i - 1, j + 1);
            }

            return _neighboursList;
        }
    }
}