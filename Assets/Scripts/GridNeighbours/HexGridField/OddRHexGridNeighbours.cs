using System.Collections.Generic;
using UnityEngine;

namespace EasyField.GridNeighbours
{
    public class OddRHexGridNeighbours<TNodeData> : GridNeighboursProviderBase<TNodeData>
    {
        protected override int Capacity => 6;

        /*
         0 1 2 3 4 5 6  //y=3
        0 1 2 3 4 5 6   //y=2
         0 1 2 3 4 5 6  //y=1
        0 1 2 3 4 5 6   //y=0
        */

        public override IReadOnlyList<TNodeData> GetNeighbours(Vector2Int index, TNodeData[,] gridItems)
        {
            _neighboursList.Clear();

            int i = index.x;
            int j = index.y;

            if (!gridItems.IsIndexWithinBounds(i, j))
                return _neighboursList;

            //left, right
            TryAddNeighbour(_neighboursList, gridItems, i - 1, j);
            TryAddNeighbour(_neighboursList, gridItems, i + 1, j);

            if ((j & 1) == 0)
            {
                //even row
                TryAddNeighbour(_neighboursList, gridItems, i - 1, j - 1); //down-left
                TryAddNeighbour(_neighboursList, gridItems, i, j - 1);     //down-right
                TryAddNeighbour(_neighboursList, gridItems, i - 1, j + 1); //up-left
                TryAddNeighbour(_neighboursList, gridItems, i, j + 1);     //up-right
            }
            else
            {
                //odd row
                TryAddNeighbour(_neighboursList, gridItems, i, j - 1);     //down-left
                TryAddNeighbour(_neighboursList, gridItems, i + 1, j - 1); //down-right
                TryAddNeighbour(_neighboursList, gridItems, i, j + 1);     //up-left
                TryAddNeighbour(_neighboursList, gridItems, i + 1, j + 1); //up-right
            }

            return _neighboursList;
        }
    }
}