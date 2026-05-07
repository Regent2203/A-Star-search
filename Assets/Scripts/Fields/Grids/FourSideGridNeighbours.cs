using Core.Nodes;
using System.Collections.Generic;

namespace Core.Fields.Grids
{
    public class FourSideGridNeighbours : IGridNeighboursProvider //up, right, down, left, no diagonal
    {
        private readonly List<INode> _neighboursList = new List<INode>(4);

        public IReadOnlyList<INode> GetNeighbours(int i, int j, INode[,] gridNodes)
        {
            _neighboursList.Clear();

            if (gridNodes.IsWithinBounds(i, j))
            {
                TryAddCell(_neighboursList, i, j + 1);
                TryAddCell(_neighboursList, i + 1, j);
                TryAddCell(_neighboursList, i, j - 1);
                TryAddCell(_neighboursList, i - 1, j);
            }

            return _neighboursList;


            void TryAddCell(List<INode> list, int i, int j)
            {
                if (gridNodes.IsWithinBounds(i, j))
                    list.Add(gridNodes[i, j]);
            }
        }
    }
}