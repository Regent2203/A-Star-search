using ThisProject.Fields.ViewMovers;
using ThisProject.Heuristic.Functions;
using ThisProject.Implementations.Vertexes;
using ThisProject.Implementations.VisualLinks;
using ThisProject.Inputs;
using ThisProject.Links.Factories;
using ThisProject.Links.Factories.CostProviders;
using ThisProject.Links.Providers;
using ThisProject.ObjectsStorages;
using ThisProject.PathDrawers;
using ThisProject.PathFinders;
using ThisProject.PathSetters;
using ThisProject.SearchAlgorithms;
using ThisProject.Starters;
using UnityEngine;
using Zenject;

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
        private VertexesClickHandler _clickHandler;
        [SerializeField]
        private VertexesDragHandler _dragHandler;
        [SerializeField]
        private VisualLink<VertexNode> _visualLinkPrefab;
        [SerializeField]
        private VertexesVisualLinksCreator _visualLinksManager;
        [SerializeField]
        private LineRenderer _pathLineRenderer;

        public override void InstallBindings()
        {
            Container.BindInstance(_inputSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<UnityInputService>().AsSingle();
            Container.BindInstance(_vertexViewPrefab).AsSingle();
            Container.BindInterfacesAndSelfTo<VertexesField>().FromInstance(_field).AsSingle();
            Container.BindInstance(_clickHandler).AsSingle();
            Container.BindInstance(_dragHandler).AsSingle();
            Container.BindInterfacesAndSelfTo<SpatialViewMover>().AsSingle();
            Container.BindInterfacesAndSelfTo<DictTypeStorage<VertexNode, int>>().AsSingle();
            Container.BindInterfacesAndSelfTo<DictTypeStorage<VertexView, int>>().AsSingle();
            Container.BindInterfacesAndSelfTo<VertexesFieldGenerator>().AsSingle();
            Container.BindInterfacesAndSelfTo<VertexViewFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<VertexNodeFactory>().AsSingle();
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
            Container.BindInterfacesAndSelfTo<PathSetter<VertexNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<PathFinder<VertexNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<LinePathDrawer>().AsSingle();
            //Container.BindInterfacesAndSelfTo<UIHotkeyInfoPanel_Vertexes>().FromInstance(_hotkeyInfoPanel).AsSingle();
            Container.BindInterfacesAndSelfTo<LineRenderer>().FromInstance(_pathLineRenderer).AsSingle();

            Container.BindInterfacesTo<Starter_Scene2a>().AsSingle();
        }
    }
}