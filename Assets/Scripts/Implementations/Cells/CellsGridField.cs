using Core.Fields.Grids;
using Core.Links;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Core.Implementations.Cells
{
    public class CellsGridField : AbstractGridField<CellNode>
    {
        private CellView[,] _views;
        private CellViewFactory _viewsFactory;
        private CellNodeFactory _nodesFactory;
        private CellsConfig _cellsConfig;
        private IGridNeighboursProvider _neighboursProvider;
        private LinkProvider _linker;


        [Inject]
        public void Construct(CellViewFactory viewsFactory, CellNodeFactory nodesFactory, CellsConfig cellsConfig, 
            IGridNeighboursProvider neighboursProvider, LinkProvider linker, CellView cellviewPrefab)
        {
            _viewsFactory = viewsFactory;
            _nodesFactory = nodesFactory;
            _cellsConfig = cellsConfig;
            _neighboursProvider = neighboursProvider;
            _linker = linker;
            _viewPrefab = cellviewPrefab;
        }

        protected override void Init()
        {
            CreateCells(); //todo: change if we want to call CreateCells() manually (after we change grid size or else)
        }

        private void CreateCells()
        {
            _views = new CellView[_nodes.GetLength(0), _nodes.GetLength(1)];
            _viewsFactory.SetConfiguration(_scaleFactor, transform);

            for (int i = 0; i < _nodes.GetLength(0); i++)
            {
                for (int j = 0; j < _nodes.GetLength(1); j++)
                {
                    var pos = _grid.CellToWorld(new Vector3Int(i, j, 0));
                    var index = new Vector2Int(i, j);

                    
                    var cellView = _viewsFactory.Create(pos, index);

                    var center = cellView.GetCenterCoords();
                    var estimatedPosition = new Vector2(center.x / _scaleFactor.x, center.y / _scaleFactor.y);
                    var cellNode = _nodesFactory.Create(estimatedPosition, index, _cellsConfig.DefaultCellType);

                    _nodes[i, j] = cellNode;
                    _views[i, j] = cellView;
                }
            }
        }

        public override IEnumerable<ILink> GetLinksForNode(CellNode node)
        {
            var index = node.Index;
            var neighbours = _neighboursProvider.GetNeighbours(index.x, index.y, _nodes);

            return _linker.GetLinks(node, neighbours);
        }
    }
}