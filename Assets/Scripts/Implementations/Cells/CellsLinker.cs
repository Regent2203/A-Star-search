using Core.CostProviders;
using Core.Fields.Grids;
using Core.Links;
using System.Collections.Generic;

namespace Core.Implementations.Cells
{
    public class CellsLinker
    {
        private readonly ICostProvider _costProvider;
        private readonly IGridNeighboursProvider<Cell> _neighboursProvider;


        public CellsLinker(ICostProvider costProvider, IGridNeighboursProvider<Cell> neighboursProvider)
        {
            _costProvider = costProvider;
            _neighboursProvider = neighboursProvider;
        }

        public void CreateLinksForCell(Cell cell1, Cell[,] gridNodes)
        {
            cell1.Links.Clear();

            if (cell1.IsBlocked)
                return;

            var neighbours = _neighboursProvider.GetNeighbours(cell1, gridNodes);
            foreach (var cell2 in neighbours)
            {
                if (cell2.IsBlocked)
                    continue;

                var cost = _costProvider.GetCost(cell1.CellType, cell2.CellType);

                var link = new Link<Cell>(cell1, cell2, cost);
                cell1.Links.Add(link);
            }
        }

        private readonly List<Cell> _cellsToUpdateList = new List<Cell>();

        public void UpdateLinksForCellAndItsNeighbours(Cell cell, Cell[,] gridNodes)
        {
            _cellsToUpdateList.Clear();
            _cellsToUpdateList.Add(cell);
            _cellsToUpdateList.AddRange(_neighboursProvider.GetNeighbours(cell, gridNodes));

            foreach (var updatingCell in _cellsToUpdateList)
                CreateLinksForCell(updatingCell, gridNodes);
        }
    }
}