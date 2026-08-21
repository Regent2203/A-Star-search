using System.Collections.Generic;
using UnityEngine;

namespace EasyField.GridNeighbours
{
    public class EvenRHexGridNeighbours<TNodeData> : GridNeighboursProviderBase<TNodeData>
    {
        protected override int Capacity => 6;

        /*
        0 1 2 3 4 5 6   //y=3
         0 1 2 3 4 5 6  //y=2
        0 1 2 3 4 5 6   //y=1
         0 1 2 3 4 5 6  //y=0
        */

        public override IReadOnlyList<TNodeData> GetNeighbours(Vector2Int index, TNodeData[,] gridItems)
        {
            _neighboursList.Clear();

            int i = index.x;
            int j = index.y;

            if (!gridItems.IsIndexWithinBounds(i, j))
                return _neighboursList;

            TryAddNeighbour(_neighboursList, gridItems, i, j - 1);
            TryAddNeighbour(_neighboursList, gridItems, i, j + 1);

            if ((i & 1) == 0)
            {
                //even
                TryAddNeighbour(_neighboursList, gridItems, i - 1, j);
                TryAddNeighbour(_neighboursList, gridItems, i - 1, j + 1);
                TryAddNeighbour(_neighboursList, gridItems, i + 1, j);
                TryAddNeighbour(_neighboursList, gridItems, i + 1, j + 1);
            }
            else
            {
                //odd
                TryAddNeighbour(_neighboursList, gridItems, i - 1, j - 1);
                TryAddNeighbour(_neighboursList, gridItems, i - 1, j);
                TryAddNeighbour(_neighboursList, gridItems, i + 1, j - 1);
                TryAddNeighbour(_neighboursList, gridItems, i + 1, j);
            }

            return _neighboursList;
        }
    }
}