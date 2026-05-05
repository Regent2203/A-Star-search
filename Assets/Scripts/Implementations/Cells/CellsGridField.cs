using Core.CostProviders;
using Core.Fields.Grids;
using Core.Links;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Core.Implementations.Cells
{
    public class CellsGridField : AbstractGridField<Cell>
    {
        public override IReadOnlyList<Cell> Nodes => _cells;

        private readonly List<Cell> _cells = new List<Cell>();
        private CellsFactory _factory;
        private CellsLinker _linker;


        [Inject]
        public void Construct(CellsFactory factory, CellsLinker linker, Cell cellPrefab)
        {
            _factory = factory;
            _linker = linker;
            _nodePrefab = cellPrefab;
        }

        protected override void Init()
        {
            CreateCells(); //todo: change if we want to call CreateCells() manually (after we change grid size or else)
        }

        private void CreateCells()
        {
            for (int i = 0; i < _gridNodes.GetLength(0); i++)
            {
                for (int j = 0; j < _gridNodes.GetLength(1); j++)
                {
                    var pos = transform.position + new Vector3((_grid.cellSize.x + _grid.cellGap.x) * i, (_grid.cellSize.y + _grid.cellGap.y) * j);

                    var cell = _factory.Create(pos, new Vector2Int(i, j), _scaleFactor, transform);
                    cell.CellTypeChanged += OnCellTypeChanged; //todo unsubscribe if I want to destroy field gameobject

                    _gridNodes[i, j] = cell;
                    _cells.Add(cell);
                }
            }

            foreach (var cell in _cells)
                _linker.CreateLinksForCell(cell, _gridNodes);
        }

        private void OnCellTypeChanged(Cell cell, CellType type)
        {
            _linker.UpdateLinksForCellAndItsNeighbours(cell, _gridNodes);
        }
    }
}