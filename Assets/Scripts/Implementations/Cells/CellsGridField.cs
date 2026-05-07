using Core.Fields.Grids;
using Core.Nodes;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Core.Implementations.Cells
{
    public class CellsGridField : AbstractGridField
    {
        private CellView[,] _views;
        private CellViewFactory _viewsFactory;
        private CellNodeFactory _nodesFactory;
        private CellsConfig _cellsConfig;
        private GridNodesLinker _linker;


        [Inject]
        public void Construct(CellViewFactory viewsFactory, CellNodeFactory nodesFactory, CellsConfig cellsConfig, GridNodesLinker linker, CellView cellviewPrefab)
        {
            _viewsFactory = viewsFactory;
            _nodesFactory = nodesFactory;
            _cellsConfig = cellsConfig;
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

            for (int i = 0; i < _nodes.GetLength(0); i++)
            {
                for (int j = 0; j < _nodes.GetLength(1); j++)
                {
                    var pos = transform.position + new Vector3((_grid.cellSize.x + _grid.cellGap.x) * i, (_grid.cellSize.y + _grid.cellGap.y) * j);
                    //var pos = _grid.CellToWorld(new Vector3Int(i, j, 0));

                    var index = new Vector2Int(i, j);

                    var cellView = _viewsFactory.Create(pos, index, _scaleFactor, transform);
                    //cellView.CellTypeChanged += OnCellTypeChanged; //todo unsubscribe if I want to destroy field gameobject

                    var estimatedPosition = new Vector2(cellView.GetCenterCoords().x / _scaleFactor.x, cellView.GetCenterCoords().y / _scaleFactor.y);

                    var cellNode = _nodesFactory.Create(estimatedPosition, index, _cellsConfig.DefaultCellType);

                    _nodes[i, j] = cellNode;
                    _views[i, j] = cellView;
                }
            }

            //foreach (var cellView in _views)
            //    _linker.CreateLinksForCell(cellView, _nodes);
        }

        private void OnCellTypeChanged(CellView cell, CellType type)
        {
            //_linker.UpdateLinksForCellAndItsNeighbours(cellView, _nodes);
        }
    }
}