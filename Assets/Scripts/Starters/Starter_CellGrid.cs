using Core.HeuristicFunctions;
using Core.Implementations.Cells;
using Core.PathFinders;
using Core.SearchAlgorithms;
using UnityEngine;
using Zenject;

namespace Core.Starters
{
    public class Starter_CellGrid : MonoBehaviour
    {
        private CellGridField _field;
        private AStarSearchAlgorithm<Cell> _searchAlgorithm;
        private ManhattanDistance _heuristicFunction;
        private PathFinder<Cell> _pathFinder;
        private CellsPathDrawer _pathDrawer;
        private CellsPainter _painter;
        private CellsMarker _marker;


        [Inject]
        public void Construct(CellGridField field, AStarSearchAlgorithm<Cell> searchAlgorithm, ManhattanDistance heuristicFunction, PathFinder<Cell> pathFinder,
            CellsPathDrawer pathDrawer, CellsPainter painter, CellsMarker marker)
        {
            _field = field;
            _searchAlgorithm = searchAlgorithm;
            _heuristicFunction = heuristicFunction;
            _pathFinder = pathFinder;
            _pathDrawer = pathDrawer;
            _painter = painter;
            _marker = marker;
        }

        private void Start()
        {
            Init();

            //field
            //recreates links each time after we finished setting obstacles
            /*
            ModeChangedPrevious += (prevMode) =>
            {
                if (prevMode == DrawMode.SelectObstacles)
                    CreateLinksForNodes();
            };*/
        }

        private void Init()
        {
            foreach (var cell in _field.Nodes)
            {
                cell.CellClicked += _painter.TryChangeCellType;
                cell.CellClicked += _marker.TryMarkCell;
                cell.CellTypeChanged += (_,_) => _pathDrawer.ShowPath(false);
            }

            _pathFinder.StartNodeSet += (cell, b) => cell.ShowStartMarker(b);
            _pathFinder.FinishNodeSet += (cell, b) => cell.ShowFinishMarker(b);
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
            else
            {
                _pathDrawer.ShowPath(false);
            }
        }
    }
}