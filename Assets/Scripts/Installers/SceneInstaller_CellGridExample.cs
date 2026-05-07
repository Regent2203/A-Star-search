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
        private CellsGridInputHandler _fieldInputHandler;
        [SerializeField]
        private CellView _cellViewPrefab;
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
            Container.BindInstance(_cellViewPrefab).AsSingle();

            Container.BindInterfacesAndSelfTo<CellsGridField>().FromInstance(_field).AsSingle();
            Container.BindInterfacesAndSelfTo<CellsGridGenerator>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsGridInputHandler>().FromInstance(_fieldInputHandler).AsSingle();
            Container.BindInterfacesAndSelfTo<CellViewFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellNodeFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<LinksProvider<CellNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<FourSideGridNeighbours<CellNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<AStarSearchAlgorithm<CellNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsHeuristicsProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<ManhattanDistance>().AsSingle();
            Container.BindInterfacesAndSelfTo<AverageCostProvider<CellNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<PathFinder<CellNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsPathDrawer>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsPainter>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsPathSetter>().AsSingle(); 
            Container.BindInterfacesAndSelfTo<UICellsPalette>().FromInstance(_palette).AsSingle();
            Container.BindInterfacesAndSelfTo<UICellsPaletteChoicePanel>().FromInstance(_paletteChoice).AsSingle();
            Container.BindInterfacesAndSelfTo<UICellsPaletteHotkeyInfoPanel>().FromInstance(_hotkeyInfoPanel).AsSingle();
            Container.BindInstance(_markingKeyCode).WithId("MarkingKey").AsSingle();
        }
    }
}