using Core.Heuristic;
using Core.Implementations.Cells;
using Core.PathFinders;
using Core.SearchAlgorithms;
using UnityEngine;
using Zenject;

namespace Core.Installers
{
    public class SceneInstaller_CellGrid : MonoInstaller
    {
        [SerializeField]
        private CellsGridField _field;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CellsGridField>().FromInstance(_field).AsSingle();            
            Container.BindInterfacesAndSelfTo<AStarSearchAlgorithm<Cell>>().AsSingle();
            Container.BindInterfacesAndSelfTo<HeuristicsProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<ManhattanDistance>().AsSingle();
            Container.BindInterfacesAndSelfTo<PathFinder<Cell>>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsPathDrawer>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsPainter>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsMarker>().AsSingle();
        }
    }
}