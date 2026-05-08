using Core.Fields.Grids;
using Core.Links;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Zenject;

namespace Core.Implementations.Cells
{
    public class CellsGridField : AbstractGridField<CellNode, CellView>
    {
        private CellsGridGenerator _generator;
        
        private IGridNeighboursProvider<CellNode> _gridNeighboursProvider;
        private LinksProvider<CellNode> _linksProvider;

        public event Action<CellNode, CellType> CellNodeTypeChanged;


        [Inject]
        public void Construct(CellsGridGenerator generator, IGridNeighboursProvider<CellNode> gridNeighboursProvider, LinksProvider<CellNode> linksProvider, CellView cellViewPrefab)
        {
            _generator = generator;
            _gridNeighboursProvider = gridNeighboursProvider;
            _linksProvider = linksProvider;
            _viewPrefab = cellViewPrefab;
        }

        protected override void Init()
        {
            //todo: change if we want to call this method not at scene start (instead: after we change grid size or else)
            _generator.PopulateField(this, transform, _scaleFactor, _grid);
        }

        public void NotifyNodeTypeChanged(CellNode node, CellType cellType)
        {
            CellNodeTypeChanged?.Invoke(node, cellType);
        }

        public CellNode GetNodeForView(CellView view) => GetNodeById(view.Index);

        public CellView GetViewForNode(CellNode node) => GetViewById(node.Index);

        public override IEnumerable<ILink<CellNode>> GetLinksForNode(CellNode node)
        {
            var index = node.Index;
            var neighbours = _gridNeighboursProvider.GetNeighbours(index.x, index.y, _nodes);

            return _linksProvider.GetLinks(node, neighbours);
        }

        public IReadOnlyList<CellView> GetViewsForNodes(IList<CellNode> nodePath) => nodePath.Select(GetViewForNode).ToList();
    }
}