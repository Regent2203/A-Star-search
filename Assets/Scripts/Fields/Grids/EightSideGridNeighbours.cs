using Core.Nodes;
using System.Collections.Generic;

namespace Core.Fields.Grids
{
    public class EightSideGridNeighbours<T, TId> : IGridNeighboursProvider<T, TId> where T : class, INode<T, TId> //all eight directions
    {
        private readonly List<T> _neighboursList = new List<T>(8);

        public IReadOnlyList<T> GetNeighbours(int i, int j, T[,] gridNodes)
        {
            _neighboursList.Clear();

            if (gridNodes.IsWithinBounds(i, j))
            {
                TryAddCell(_neighboursList, i, j + 1);
                TryAddCell(_neighboursList, i + 1, j + 1);
                TryAddCell(_neighboursList, i + 1, j);
                TryAddCell(_neighboursList, i + 1, j - 1);
                TryAddCell(_neighboursList, i, j - 1);
                TryAddCell(_neighboursList, i - 1, j - 1);
                TryAddCell(_neighboursList, i - 1, j);
                TryAddCell(_neighboursList, i - 1, j + 1);
            }

            return _neighboursList;


            void TryAddCell(List<T> list, int i, int j)
            {
                if (gridNodes.IsWithinBounds(i, j))
                    list.Add(gridNodes[i, j]);
            }
        }
    }
}