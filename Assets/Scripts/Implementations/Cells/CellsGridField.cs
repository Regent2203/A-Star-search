using Core.Fields.Grids;
using Core.Links;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core.Implementations.Cells
{
    public class CellsGridField : AbstractGridField<CellNode>
    {
        private CellsGridGenerator _generator;
        private CellView[,] _views;
        private IGridNeighboursProvider<CellNode> _neighboursProvider;
        private LinksProvider<CellNode> _linksProvider;

        public event Action<CellNode> CellNodeChanged;


        [Inject]
        public void Construct(CellsGridGenerator generator, IGridNeighboursProvider<CellNode> neighboursProvider, LinksProvider<CellNode> linker, CellView cellviewPrefab)
        {
            _generator = generator;
            _neighboursProvider = neighboursProvider;
            _linksProvider = linker;
            _viewPrefab = cellviewPrefab;
        }

        protected override void Init()
        {
            //todo: change if we want to call this method not at scene start (instead: after we change grid size or else)
            _generator.PopulateField(this, transform, _scaleFactor, _grid);
        }

        public void SetData(CellNode[,] nodes, CellView[,] views)
        {
            _nodes = nodes;
            _views = views;
        }

        public CellView GetViewByIndex(int i, int j)
        {
            if (_views.IsWithinBounds(i, j))
                return _views[i, j];

            return null;
        }

        public void NotifyNodeChanged(CellNode node)
        {
            CellNodeChanged?.Invoke(node);
        }

        public override IEnumerable<ILink<CellNode>> GetLinksForNode(CellNode node)
        {
            var index = node.Index;
            var neighbours = _neighboursProvider.GetNeighbours(index.x, index.y, _nodes);

            return _linksProvider.GetLinks(node, neighbours);
        }
    }
}