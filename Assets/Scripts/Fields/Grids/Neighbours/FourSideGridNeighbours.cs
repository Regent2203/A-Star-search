using Core.Nodes;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Fields.Grids.Neighbours
{
    public class FourSideGridNeighbours<T> : GridNeighboursProviderBase<T> where T : class, INode<T, Vector2Int> //up, right, down, left, no diagonal
    {
        private readonly List<T> _neighboursList = new List<T>(4);

        public override IReadOnlyList<T> GetNeighbours(Vector2Int index, T[,] gridNodes)
        {
            _neighboursList.Clear();

            int i = index.x;
            int j = index.y;

            if (gridNodes.IsWithinBounds(i, j))
            {
                TryAddCell(_neighboursList, gridNodes, i, j + 1);
                TryAddCell(_neighboursList, gridNodes, i + 1, j);
                TryAddCell(_neighboursList, gridNodes, i, j - 1);
                TryAddCell(_neighboursList, gridNodes, i - 1, j);
            }

            return _neighboursList;
        }
    }
}