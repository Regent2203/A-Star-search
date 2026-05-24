using ThisProject.Links.Factories.CostProviders;
using ThisProject.Heuristic.Functions;
using ThisProject.Implementations.Vertexes;
using ThisProject.Links.Factories;
using ThisProject.Links.Providers;
using ThisProject.PathDrawers;
using ThisProject.PathFinders;
using ThisProject.SearchAlgorithms;
using UnityEngine;
using Zenject;
using ThisProject.Implementations.VisualLinks;
using ThisProject.Implementations.Cells;
using ThisProject.Starters;
using ThisProject.Inputs;
using ThisProject.Fields.Grids;
using ThisProject.Fields;

namespace ThisProject.Installers
{
    public class SceneInstaller_Scene2a : MonoInstaller
    {
        [SerializeField]
        private InputSettings _inputSettings;
        [SerializeField]
        private VertexView _vertexViewPrefab;
        [SerializeField]
        private VertexesField _field;
        [SerializeField]
        private VisualLink<VertexNode> _visualLinkPrefab;
        [SerializeField]
        private VertexesVisualLinksCreator _visualLinksManager;
        //[SerializeField]
        //private UIHotkeyInfoPanel_Vertexes _hotkeyInfoPanel;
        [SerializeField]
        private LineRenderer _pathLineRenderer;

        public override void InstallBindings()
        {
            Container.BindInstance(_inputSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<UnityInputService>().AsSingle();
            Container.BindInstance(_vertexViewPrefab).AsSingle();
            Container.BindInterfacesAndSelfTo<VertexesField>().FromInstance(_field).AsSingle();
            Container.BindInterfacesAndSelfTo<VertexesFieldGenerator>().AsSingle();
            Container.BindInterfacesAndSelfTo<VertexViewFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<VertexNodeFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<FieldClickHandler<VertexNode, VertexView, int>>().AsSingle(); 
            Container.BindInstance(_visualLinkPrefab).AsSingle();
            Container.BindInterfacesAndSelfTo<VertexesVisualLinksCreator>().FromInstance(_visualLinksManager).AsSingle();
            Container.BindInterfacesAndSelfTo<VisualLinksFactory<VertexNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<VisualLinksPool<VertexNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<StoredLinksProvider<VertexNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<LinksFactory<VertexNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<AStarSearchAlgorithm<VertexNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<VertexesHeuristicsProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<EuclideanDistance>().AsSingle();
            Container.BindInterfacesAndSelfTo<ConstantCostProvider<VertexNode>>().AsSingle().WithArguments(0.0f);
            Container.BindInterfacesAndSelfTo<PathFinder<VertexNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<LinePathDrawer>().AsSingle();
            //Container.BindInterfacesAndSelfTo<UIHotkeyInfoPanel_Vertexes>().FromInstance(_hotkeyInfoPanel).AsSingle();
            Container.BindInterfacesAndSelfTo<LineRenderer>().FromInstance(_pathLineRenderer).AsSingle();

            Container.BindInterfacesTo<Starter_Scene2a>().AsSingle();
        }
    }
}