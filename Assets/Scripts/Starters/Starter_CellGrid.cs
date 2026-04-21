using UnityEngine;
using Core.Fields;
using Core.SearchAlgorithms;
using Core.HeuristicFunctions;
using Zenject;
using Core.PathDrawers;
using Core.PathFinders;
using Core.Nodes;
using Core.Nodes.Cells;

namespace Core.Starters
{
    public class Starter_CellGrid : MonoBehaviour
    {
        private AbstractField _field;
        private ISearchAlgorithm<Cell> _searchAlgorithm;
        private IHeuristicFunction _heuristicFunction;
        private PathFinder _pathFinder;
        private CellGridPathDrawer _pathDrawer;


        [Inject]
        public void Construct(AbstractField field, ISearchAlgorithm<Cell> searchAlgorithm, IHeuristicFunction heuristicFunction, PathFinder pathFinder, CellGridPathDrawer pathDrawer)
        {
            _field = field;
            _searchAlgorithm = searchAlgorithm;
            _heuristicFunction = heuristicFunction;
            _pathFinder = pathFinder;
            _pathDrawer = pathDrawer;
        }

        private void Start()
        {
            var path = _pathFinder.GetPath();
            _pathDrawer.SetPath(path);

            //_field.StartFinishReady += RunAlgorithm;
        }

        private void RunAlgorithm()
        {
            //            
        }
    }
}