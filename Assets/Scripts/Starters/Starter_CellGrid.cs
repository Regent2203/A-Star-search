using Core.Heuristic;
using Core.Implementations.Cells;
using Core.PathFinders;
using Core.SearchAlgorithms;
using UnityEngine;
using Zenject;

namespace Core.Starters
{
    public class Starter_CellGrid : MonoBehaviour
    {
        private CellsGridField _field;
        private AStarSearchAlgorithm<Cell> _searchAlgorithm;
        private HeuristicsProvider _heuristicsProvider;
        private ManhattanDistance _heuristicFunction;
        private PathFinder<Cell> _pathFinder;
        private CellsPathDrawer _pathDrawer;
        private CellsPainter _painter;
        private CellsMarker _marker;


        [Inject]
        public void Construct(CellsGridField field, AStarSearchAlgorithm<Cell> searchAlgorithm, HeuristicsProvider heuristicsController, ManhattanDistance heuristicFunction,
            PathFinder<Cell> pathFinder, CellsPathDrawer pathDrawer, CellsPainter painter, CellsMarker marker)
        {
            _field = field;
            _searchAlgorithm = searchAlgorithm;
            _heuristicsProvider = heuristicsController;
            _heuristicFunction = heuristicFunction;
            _pathFinder = pathFinder;
            _pathDrawer = pathDrawer;
            _painter = painter;
            _marker = marker;
        }

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            foreach (var cell in _field.Nodes)
            {
                cell.CellClicked += _painter.TryChangeCellType;
                cell.CellClicked += _marker.TryMarkCell;
                cell.CellTypeChanged += (cell, _) => _heuristicsProvider.SetMinimumStepCost(cell.CellType.Weight, false);
                cell.CellTypeChanged += (_, _) => _pathDrawer.ShowPath(false);
                cell.CellTypeChanged += (_, _) => _pathFinder.CheckStartAndFinishReady();
            }
            
            _pathFinder.NodeChanged += () => _pathDrawer.ShowPath(false);
            
            _pathFinder.StartNodeChanged += (cell, b) => cell?.ShowStartMarker(b);
            _pathFinder.FinishNodeChanged += (cell, b) => cell?.ShowFinishMarker(b);
            
            _pathFinder.StartAndFinishReady += Run;
        }

        private void Run(bool isReady)
        {
            if (isReady)
            {
                var path = _pathFinder.GetPath();
                _pathDrawer.SetPath(path);
                _pathDrawer.ShowPath(true);
            }
        }
    }
}