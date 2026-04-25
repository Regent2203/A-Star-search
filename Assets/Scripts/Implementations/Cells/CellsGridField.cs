using Core.Fields;
using Core.Links;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Core.Implementations.Cells
{
    public class CellsGridField : AbstractGridField<Cell>
    {
        [SerializeField]
        private Cell _cellViewPrefab;

        protected override IView _nodePrefab => _cellViewPrefab;
        public override List<Cell> Nodes => _cells;

        private List<Cell> _cells = new List<Cell>();
        private IInstantiator _instantiator;


        [Inject]
        public void Construct(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        protected override void Awake()
        {
            base.Awake();
            
            CreateCells();
        }

        private void CreateCells()
        {
            for (int i = 0; i < _gridNodes.GetLength(0); i++)
            {
                for (int j = 0; j < _gridNodes.GetLength(1); j++)
                {
                    var cell = _instantiator.InstantiatePrefabForComponent<Cell>(
                        _cellViewPrefab,
                        transform.position + new Vector3(_grid.cellSize.x * i, _grid.cellSize.y * j, 0) + new Vector3(_grid.cellGap.x * i, _grid.cellGap.y * j),
                        Quaternion.identity, transform);

                    cell.Init(new Vector2Int(i, j), _scaleFactor);
                    cell.CellTypeChanged += (cell, _) => UpdateLinksForCellAndItsNeighbours(cell);

                    _gridNodes[i, j] = cell;
                    _cells.Add(cell);
                }
            }

            foreach (var cell in _cells)
                CreateLinksForCell(cell);
        }

        private void UpdateLinksForCellAndItsNeighbours(Cell cell)
        {
            var cellsToUpdate = new List<Cell>();
            cellsToUpdate.Add(cell);
            cellsToUpdate.AddRange(GetCellNeighbours(cell));

            foreach (var updatingCell in cellsToUpdate)
                CreateLinksForCell(updatingCell);
        }

        private List<Cell> GetCellNeighbours(Cell cell) //up, down, left, right, no diagonal
        {
            var neighbours = new List<Cell>();

            TryAddCell(neighbours, cell.Index.x, cell.Index.y + 1);
            TryAddCell(neighbours, cell.Index.x, cell.Index.y - 1);
            TryAddCell(neighbours, cell.Index.x - 1, cell.Index.y);
            TryAddCell(neighbours, cell.Index.x + 1, cell.Index.y);

            return neighbours;


            void TryAddCell(List<Cell> list, int i, int j)
            {
                if (_gridNodes.IndexExists(i, 0) && _gridNodes.IndexExists(j, 1))
                    list.Add(_gridNodes[i, j]);
            }
        }

        private void CreateLinksForCell(Cell cell1) 
        {
            cell1.Links.Clear();

            if (cell1.IsBlocked)
                return;

            var neighbours = GetCellNeighbours(cell1);
            foreach (var cell2 in neighbours)
            {
                if (cell2.IsBlocked)
                    continue;

                var weight = cell1.CellType.Weight / 2 + cell2.CellType.Weight / 2;

                var link = new Link<Cell>(cell1, cell2, weight);
                cell1.Links.Add(link);
            }
        }
    }
}