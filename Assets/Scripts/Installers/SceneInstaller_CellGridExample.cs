using Core.CostProviders;
using Core.Fields.Grids;
using Core.Heuristic;
using Core.Implementations.Cells;
using Core.Implementations.Cells.UI;
using Core.PathFinders;
using Core.SearchAlgorithms;
using UnityEngine;
using Zenject;

namespace Core.Installers
{
    public class SceneInstaller_CellGridExample : MonoInstaller
    {
        [SerializeField]
        private CellsGridField _field;
        [SerializeField]
        private Cell _cellPrefab;
        [SerializeField]
        private UICellsPalette _palette;
        [SerializeField]
        private UICellsPaletteChoicePanel _paletteChoice;
        [SerializeField]
        private UICellsPaletteHotkeyInfoPanel _hotkeyInfoPanel;
        [SerializeField]
        private KeyCode _markingKeyCode = KeyCode.LeftShift;

        public override void InstallBindings()
        {
            Container.BindInstance(_cellPrefab).AsSingle();

            Container.BindInterfacesAndSelfTo<CellsGridField>().FromInstance(_field).AsSingle();
            Container.BindInterfacesAndSelfTo<CellsFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsLinker>().AsSingle();
            Container.BindInterfacesAndSelfTo<FourSideGridNeighbours>().AsSingle();
            Container.BindInterfacesAndSelfTo<AStarSearchAlgorithm<Cell>>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsHeuristicsProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<ManhattanDistance>().AsSingle();
            Container.BindInterfacesAndSelfTo<AverageCostProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<PathFinder<Cell>>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsPathDrawer>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsPainter>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsMarker>().AsSingle(); 
            Container.BindInterfacesAndSelfTo<UICellsPalette>().FromInstance(_palette).AsSingle();
            Container.BindInterfacesAndSelfTo<UICellsPaletteChoicePanel>().FromInstance(_paletteChoice).AsSingle();
            Container.BindInterfacesAndSelfTo<UICellsPaletteHotkeyInfoPanel>().FromInstance(_hotkeyInfoPanel).AsSingle();
            Container.BindInstance(_markingKeyCode).WithId("MarkingKey").AsSingle();
        }
    }
}