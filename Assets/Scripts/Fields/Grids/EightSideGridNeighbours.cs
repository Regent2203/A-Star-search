using Core.Implementations.Cells;
using System.Collections.Generic;

namespace Core.Fields.Grids
{
    public class EightSideGridNeighbours : IGridNeighboursProvider<Cell> //all eight directions
    {
        private readonly List<Cell> _neighboursList = new List<Cell>(8);

        public IReadOnlyList<Cell> GetNeighbours(Cell node, Cell[,] gridNodes)
        {
            _neighboursList.Clear();

            TryAddCell(_neighboursList, node.Index.x, node.Index.y + 1);
            TryAddCell(_neighboursList, node.Index.x + 1, node.Index.y + 1);
            TryAddCell(_neighboursList, node.Index.x + 1, node.Index.y);
            TryAddCell(_neighboursList, node.Index.x + 1, node.Index.y - 1);
            TryAddCell(_neighboursList, node.Index.x, node.Index.y - 1);
            TryAddCell(_neighboursList, node.Index.x - 1, node.Index.y - 1);
            TryAddCell(_neighboursList, node.Index.x - 1, node.Index.y);
            TryAddCell(_neighboursList, node.Index.x - 1, node.Index.y + 1);

            return _neighboursList;


            void TryAddCell(List<Cell> list, int i, int j)
            {
                if (gridNodes.IsWithinBounds(i, j))
                    list.Add(gridNodes[i, j]);
            }
        }
    }
}