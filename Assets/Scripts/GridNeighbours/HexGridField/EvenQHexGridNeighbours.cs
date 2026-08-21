using System.Collections.Generic;
using UnityEngine;

namespace EasyField.GridNeighbours
{
    public class EvenQHexGridNeighbours<TNodeData> : GridNeighboursProviderBase<TNodeData>
    {
        protected override int Capacity => 6;

        /*
        2   2  //y=2
          2    //y=2
        1   1  //y=1
          1    //y=1
        0   0  //y=0
          0    //y=0
        */

        public override IReadOnlyList<TNodeData> GetNeighbours(Vector2Int index, TNodeData[,] gridItems)
        {
            _neighboursList.Clear();

            int i = index.x;
            int j = index.y;

            if (!gridItems.IsIndexWithinBounds(i, j))
                return _neighboursList;

            //up, down
            TryAddNeighbour(_neighboursList, gridItems, i, j - 1);
            TryAddNeighbour(_neighboursList, gridItems, i, j + 1);

            if ((i & 1) == 0)
            {
                //even column
                TryAddNeighbour(_neighboursList, gridItems, i - 1, j);     //down-left
                TryAddNeighbour(_neighboursList, gridItems, i - 1, j + 1); //up-left
                TryAddNeighbour(_neighboursList, gridItems, i + 1, j);     //down-right
                TryAddNeighbour(_neighboursList, gridItems, i + 1, j + 1); //up-right
            }
            else
            {
                //odd column
                TryAddNeighbour(_neighboursList, gridItems, i - 1, j - 1); //down-left
                TryAddNeighbour(_neighboursList, gridItems, i - 1, j);     //up-left
                TryAddNeighbour(_neighboursList, gridItems, i + 1, j - 1); //down-right
                TryAddNeighbour(_neighboursList, gridItems, i + 1, j);     //up-right
            }

            return _neighboursList;
        }
    }
}
