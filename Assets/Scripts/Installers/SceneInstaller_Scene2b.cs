using EasyField.Fields;
using EasyField.Heuristic;
using EasyField.Implementations.Links;
using EasyField.Implementations.Vertexes;
using EasyField.Implementations.Vertexes.Core.Dto;
using EasyField.Implementations.Vertexes.UI;
using EasyField.Inputs;
using EasyField.Links;
using EasyField.Links.CostProviders;
using EasyField.Links.Factories;
using EasyField.Links.Implementations;
using EasyField.Links.LinkCostChangers;
using EasyField.Links.Providers;
using EasyField.Links.ViewMovers;
using EasyField.Nodes.NodeBlockers;
using EasyField.Nodes.NodePositionChanger;
using EasyField.Nodes.ViewMovers;
using EasyField.Nodes.ViewSelectors;
using EasyField.ObjectsStorages;
using EasyField.PathDrawers;
using EasyField.PathFinders;
using EasyField.PathSetters;
using EasyField.SaveSystem;
using EasyField.SaveSystem.FileDtoGateways;
using EasyField.SaveSystem.FilePathProviders;
using EasyField.SceneControllers;
using EasyField.SearchAlgorithms;
using EasyField.Serializers;
using EasyField.UICommon;
using System;
using UnityEngine;
using Zenject;

namespace EasyField.Installers
{
    public class SceneInstaller_Scene2b : MonoInstaller
    {
        [SerializeField]
        private Camera _mainCamera;
        [SerializeField]
        private InputSettings _inputSettings;
        [SerializeField]
        private VertexView _vertexViewPrefab;
        [SerializeField]
        private SpatialField _field;
        [SerializeField]
        private VertexesLinksClickHandler _clickHandler;
        [SerializeField]
        private VertexesDragHandler _dragHandler;
        [SerializeField]
        private LinkView_Int _linkViewPrefab;
        [SerializeField]
        private LineRenderer _pathLineRenderer;
        [SerializeField]
        private UIHotkeysInfoPanel_Vertexes _hotkeyInfoPanel;
        [SerializeField]
        private UIMainButtonsPanel _saveLoadPanel;


        public override void InstallBindings()
        {
            BindMainComponents();
            BindEnviroment();
            BindNodes();
            BindLinks();
            BindManipulators();
            BindPathfinding();
            BindSaveSystem();
            BindUI();
        }

        private void BindMainComponents()
        {
            Container.BindInterfacesAndSelfTo<SceneController_Scene2b>().AsSingle();
            Container.BindInterfacesAndSelfTo<VertexesSaveLoadManager>().AsSingle();            

            Container.BindInterfacesAndSelfTo<SpatialField>().FromInstance(_field).AsSingle();
            Container.BindInterfacesAndSelfTo<VertexesFieldBuilder>().AsSingle();
            Container.BindInterfacesAndSelfTo<VertexesNodesBuilder>().AsSingle();
            Container.BindInterfacesAndSelfTo<VertexesLinksBuilder>().AsSingle().WithArguments(true);
        }

        private void BindEnviroment()
        {
            Container.BindInstance(_mainCamera).AsSingle();
            Container.BindInstance(_inputSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<UnityInputService>().AsSingle();            
        }

        private void BindNodes()
        {            
            Container.Bind(typeof(VertexDataStorage), typeof(DictTypeStorage<VertexData, int>), typeof(IObjectsStorage<VertexData, int>)).
                To<VertexDataStorage>().AsSingle();
            Container.Bind(typeof(VertexViewStorage), typeof(DictTypeStorage<VertexView, int>), typeof(IObjectsStorage<VertexView, int>)).
                To<VertexViewStorage>().AsSingle();

            Container.BindInterfacesAndSelfTo<VertexDataFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<VertexViewFactory>().AsSingle();
            Container.BindMemoryPool<VertexData, VertexDataPool>().WithInitialSize(20);
            Container.BindMemoryPool<VertexView, VertexViewPool>().WithInitialSize(20).
                FromComponentInNewPrefab(_vertexViewPrefab).UnderTransform(_field.NodesContainer);            
        }

        private void BindLinks()
        {
            Container.Bind(typeof(LinkDataStorage_Int), 
                typeof(DictTypeStorage<LinkData<int>, DualKey<int>>), typeof(IObjectsStorage<LinkData<int>, DualKey<int>>)).
                To<LinkDataStorage_Int>().AsSingle();
            Container.Bind(typeof(LinkViewStorage_Int),
                typeof(DictTypeStorage<LinkView<int>, DualKey<int>>), typeof(IObjectsStorage<LinkView<int>, DualKey<int>>)).
                To<LinkViewStorage_Int>().AsSingle();

            Container.BindInterfacesAndSelfTo<SmartLinkDataFactory<VertexData, int>>().AsSingle();
            Container.BindInterfacesAndSelfTo<LinkViewFactory<int>>().AsSingle();
            Container.BindMemoryPool<LinkData<int>, LinkDataPool<int>>().WithInitialSize(20);
            Container.BindMemoryPool<LinkView<int>, LinkViewPool<int>>().WithInitialSize(20).
                FromComponentInNewPrefab(_linkViewPrefab).UnderTransform(_field.LinksContainer);

            Container.BindInterfacesAndSelfTo<StoredLinksProvider<LinkData<int>, int>>().AsSingle();
            
        }

        private void BindManipulators()
        {
            Container.BindInstance(_clickHandler).AsSingle();
            Container.BindInstance(_dragHandler).AsSingle();
            Container.BindInterfacesAndSelfTo<NodePositionChanger<VertexData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<NodeBlocker<VertexData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<NodeViewSelector<VertexView>>().AsSingle();
            Container.BindInterfacesAndSelfTo<NodeViewMover<VertexView>>().AsSingle();
            Container.BindInterfacesAndSelfTo<LinkViewCoordinator<VertexView, int>>().AsSingle();
            Container.BindInterfacesAndSelfTo<LinkCostAdder<LinkData<int>>>().AsSingle();
        }

        private void BindPathfinding()
        {
            Container.BindInterfacesAndSelfTo<AStarSearchAlgorithm<VertexData, LinkData<int>, int>>().AsSingle();
            Container.BindInterfacesAndSelfTo<DijkstraHeuristicsProvider<VertexData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<ConstantCostProvider<VertexData>>().AsSingle().WithArguments(1.0f);
            Container.BindInterfacesAndSelfTo<PathSetter<VertexData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<PathFinder<VertexData, int>>().AsSingle();
            Container.BindInterfacesAndSelfTo<LinePathDrawer<VertexView>>().AsSingle();
            Container.Bind<LineRenderer>().WithId(LinePathDrawer.LineRendererId).FromInstance(_pathLineRenderer).AsSingle();
        }

        private void BindSaveSystem()
        {
            Container.BindInterfacesAndSelfTo<Saver>().AsSingle();
            Container.BindInterfacesAndSelfTo<Loader>().AsSingle();

            Container.BindInterfacesAndSelfTo<VertexDataMapper>().AsSingle();
            Container.BindInterfacesAndSelfTo<LinkDataMapper>().AsSingle();
            Container.BindInterfacesAndSelfTo<VertexesFieldSaveDtoProvider>().AsSingle();

            Container.BindInterfacesAndSelfTo<StringFileDtoGateway>().AsSingle();
            Container.BindInterfacesAndSelfTo<JsonUtilityStringSerializer>().AsSingle();

            //Choose only one
            //Container.BindInterfacesAndSelfTo<DialogueFilePathProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<ConstantFilePathProvider>().AsSingle().WithArguments("Map_2b.json", Environment.SpecialFolder.Desktop);            
        }

        private void BindUI()
        {
            Container.BindInterfacesAndSelfTo<UIHotkeysInfoPanel_Vertexes>().FromInstance(_hotkeyInfoPanel).AsSingle();
            Container.BindInterfacesAndSelfTo<UIMainButtonsPanel>().FromInstance(_saveLoadPanel).AsSingle();
        }
    }
}