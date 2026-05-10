using Core.Nodes;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Fields.Grids.Neighbours
{
    public class EightSideGridNeighbours<T> : GridNeighboursProviderBase<T> where T : class, INode<Vector2Int> //all eight directions
    {
        private readonly List<T> _neighboursList = new List<T>(8);

        public override IReadOnlyList<T> GetNeighbours(Vector2Int index, T[,] gridNodes)
        {
            _neighboursList.Clear();

            int i = index.x;
            int j = index.y;

            if (gridNodes.IsWithinBounds(i, j))
            {
                TryAddCell(_neighboursList, gridNodes, i, j + 1);
                TryAddCell(_neighboursList, gridNodes, i + 1, j + 1);
                TryAddCell(_neighboursList, gridNodes, i + 1, j);
                TryAddCell(_neighboursList, gridNodes, i + 1, j - 1);
                TryAddCell(_neighboursList, gridNodes, i, j - 1);
                TryAddCell(_neighboursList, gridNodes, i - 1, j - 1);
                TryAddCell(_neighboursList, gridNodes, i - 1, j);
                TryAddCell(_neighboursList, gridNodes, i - 1, j + 1);
            }

            return _neighboursList;
        }
    }
}