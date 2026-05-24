using System.Collections.Generic;
using UnityEngine;

namespace ThisProject.Fields.Grids.Neighbours
{
    public class EightSideGridNeighbours<T> : GridNeighboursProviderBase<T> //all eight directions
    {
        private readonly List<T> _neighboursList = new List<T>(8);

        public override IReadOnlyList<T> GetNeighbours(Vector2Int index, T[,] gridItems)
        {
            _neighboursList.Clear();

            int i = index.x;
            int j = index.y;

            if (gridItems.IsWithinBounds(i, j))
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