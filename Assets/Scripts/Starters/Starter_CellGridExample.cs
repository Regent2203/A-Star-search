using Core.Heuristic;
using Core.Implementations.Cells;
using Core.PathFinders;
using Core.SearchAlgorithms;
using UnityEngine;
using Zenject;

namespace Core.Starters
{
    public class Starter_CellGridExample : MonoBehaviour
    {
        private CellsGridField _field;
        private AStarSearchAlgorithm<Cell> _searchAlgorithm;
        private CellsHeuristicsProvider _heuristicsProvider;
        private ManhattanDistance _heuristicFunction;
        private PathFinder<Cell> _pathFinder;
        private CellsPathDrawer _pathDrawer;
        private CellsPainter _painter;


        [Inject]
        public void Construct(CellsGridField field, AStarSearchAlgorithm<Cell> searchAlgorithm, CellsHeuristicsProvider heuristicsController, ManhattanDistance heuristicFunction,
            PathFinder<Cell> pathFinder, CellsPathDrawer pathDrawer, CellsPainter painter)
        {
            _field = field;
            _searchAlgorithm = searchAlgorithm;
            _heuristicsProvider = heuristicsController;
            _heuristicFunction = heuristicFunction;
            _pathFinder = pathFinder;
            _pathDrawer = pathDrawer;
            _painter = painter;
        }

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            foreach (var cell in _field.Nodes)
            {
                cell.CellClicked += _painter.TryChangeCell;
                cell.CellTypeChanged += (_, _) => _pathDrawer.ShowPath(false);
                cell.CellTypeChanged += (_, _) => TryRun();
            }
            
            _pathFinder.NodeChanged += () => _pathDrawer.ShowPath(false);
            
            _pathFinder.StartNodeChanged += (cell, b) => cell?.ShowStartMarker(b);
            _pathFinder.FinishNodeChanged += (cell, b) => cell?.ShowFinishMarker(b);
            
            _pathFinder.NodeChanged += TryRun;
        }

        private void TryRun()
        {
            if (_pathFinder.IsReady)
            {
                var path = _pathFinder.GetPath();
                _pathDrawer.SetPath(path);
                _pathDrawer.ShowPath(true);
            }
        }
    }
}