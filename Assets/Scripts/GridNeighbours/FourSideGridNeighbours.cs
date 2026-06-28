using System.Collections.Generic;
using UnityEngine;

namespace ThisProject.Fields.GridNeighbours
{
    public class FourSideGridNeighbours<T> : GridNeighboursProviderBase<T> //up, right, down, left, no diagonal
    {
        private readonly List<T> _neighboursList = new List<T>(4);

        public override IReadOnlyList<T> GetNeighbours(Vector2Int index, T[,] gridItems)
        {
            _neighboursList.Clear();

            int i = index.x;
            int j = index.y;

            if (gridItems.IsIndexWithinBounds(i, j))
            {
                TryAddCell(_neighboursList, gridItems, i, j + 1);
                TryAddCell(_neighboursList, gridItems, i + 1, j);
                TryAddCell(_neighboursList, gridItems, i, j - 1);
                TryAddCell(_neighboursList, gridItems, i - 1, j);
            }

            return _neighboursList;
        }
    }
}