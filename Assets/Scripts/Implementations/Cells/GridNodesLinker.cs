using Core.CostProviders;
using Core.Links;
using Core.Nodes;
using System.Collections.Generic;

namespace Core.Fields.Grids
{
    public class GridNodesLinker
    {
        private readonly AbstractGridField _gridField;
        private readonly IGridNeighboursProvider _gridNeighboursProvider;
        private readonly ICostProvider _costProvider;

        /*
        public GridNodesLinker(AbstractGridField gridField, IGridNeighboursProvider gridNeighboursProvider, ICostProvider costProvider)
        {
            _gridField = gridField;            
            _gridNeighboursProvider = gridNeighboursProvider;
            _costProvider = costProvider;
        }*/
        /*
        public void CreateLinksForCell(INode node1, INode[,] gridNodes)
        {
            node1.Links.Clear();

            if (node1.IsBlocked)
                return;

            var neighbours = _gridNeighboursProvider.GetNeighbours(node1, gridNodes);
            foreach (var cell2 in neighbours)
            {
                if (cell2.IsBlocked)
                    continue;

                var cost = _costProvider.GetCost(node1.CellType, cell2.CellType);

                var link = new Link(node1, cell2, cost);
                node1.Links.Add(link);
            }
        }

        private readonly List<INode> _cellsToUpdateList = new List<INode>();

        public void UpdateLinksForCellAndItsNeighbours(INode cell, INode[,] gridNodes)
        {
            _cellsToUpdateList.Clear();
            _cellsToUpdateList.Add(cell);
            _cellsToUpdateList.AddRange(_gridNeighboursProvider.GetNeighbours(cell, gridNodes));

            foreach (var updatingCell in _cellsToUpdateList)
                CreateLinksForCell(updatingCell, gridNodes);
        }*/
    }
}