using System;
using ThisProject.Fields;
using ThisProject.Heuristic.Functions;
using ThisProject.Implementations.Vertexes;
using ThisProject.Inputs;
using ThisProject.Links;
using ThisProject.Links.CostProviders;
using ThisProject.Links.Factories;
using ThisProject.Links.Implementations;
using ThisProject.Links.LinkCostChangers;
using ThisProject.Links.Providers;
using ThisProject.Links.ViewMovers;
using ThisProject.Nodes.NodeBlockers;
using ThisProject.Nodes.ViewMovers;
using ThisProject.Nodes.ViewSelectors;
using ThisProject.ObjectsStorages;
using ThisProject.PathDrawers;
using ThisProject.PathFinders;
using ThisProject.PathSetters;
using ThisProject.SaveSystem;
using ThisProject.SaveSystem.DtoFileIOs;
using ThisProject.SaveSystem.FilePathProviders;
using ThisProject.SaveSystem.Serializers;
using ThisProject.SearchAlgorithms;
using ThisProject.Starters;
using ThisProject.UICommon;
using UnityEngine;
using Zenject;

namespace ThisProject.Installers
{
    public class SceneInstaller_Scene2a : MonoInstaller
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
        private VertexesClickHandler _clickHandler;
        [SerializeField]
        private VertexesDragHandler _dragHandler;
        [SerializeField]
        private LinkView_Int _linkViewPrefab;
        [SerializeField]
        private VertexesLinksBuilder _visualLinksManager;
        [SerializeField]
        private LineRenderer _pathLineRenderer;
        [SerializeField]
        private UISaveLoadPanel _saveLoadPanel;


        public override void InstallBindings()
        {
            BindStarter();
            BindEnviroment();
            BindNodes();
            BindPathfinding();
            BindLinks();
            BindManipulators();
            BindSaveSystem();
            BindUI();
        }

        private void BindStarter()
        {
            Container.BindInterfacesAndSelfTo<Starter_Scene2a>().AsSingle();

            Container.BindInterfacesAndSelfTo<SpatialField>().FromInstance(_field).AsSingle();
            Container.BindInterfacesAndSelfTo<VertexesFieldBuilder>().AsSingle();
            Container.BindInterfacesAndSelfTo<VertexesNodesBuilder>().AsSingle();
            Container.BindInterfacesAndSelfTo<VertexesLinksBuilder>().AsSingle();
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
            
            Container.BindMemoryPool<VertexData, VertexDataPool>().WithInitialSize(20);
            Container.BindMemoryPool<VertexView, VertexViewPool>().WithInitialSize(20).
                FromComponentInNewPrefab(_vertexViewPrefab).UnderTransform(_field.NodesContainer);            
        }

        private void BindLinks()
        {
            Container.Bind(typeof(LinkDataStorage_Int), 
                typeof(DictTypeStorage<LinkData<int>, LinkKey<int>>), typeof(IObjectsStorage<LinkData<int>, LinkKey<int>>)).
                To<LinkDataStorage_Int>().AsSingle();
            Container.Bind(typeof(LinkViewStorage_Int),
                typeof(DictTypeStorage<LinkView<int>, LinkKey<int>>), typeof(IObjectsStorage<LinkView<int>, LinkKey<int>>)).
                To<LinkViewStorage_Int>().AsSingle();

            Container.BindMemoryPool<LinkData<int>, LinkDataPool<int>>().WithInitialSize(20);
            Container.BindMemoryPool<LinkView<int>, LinkViewPool<int>>().WithInitialSize(20).
                FromComponentInNewPrefab(_linkViewPrefab).UnderTransform(_field.LinksContainer);

            Container.BindInterfacesAndSelfTo<StoredLinksProvider<LinkData<int>, int>>().AsSingle();
            Container.BindInterfacesAndSelfTo<LinkViewCoordinator<VertexView, int>>().AsSingle();
            Container.BindInterfacesAndSelfTo<LinkDataFactory<VertexData, int>>().AsSingle();
            Container.BindInterfacesAndSelfTo<LinkViewFactory<int>>().AsSingle();

            Container.BindInterfacesAndSelfTo<LinkCostSetter<LinkData<int>>>().AsSingle();
        }

        private void BindManipulators()
        {
            Container.BindInstance(_clickHandler).AsSingle();
            Container.BindInstance(_dragHandler).AsSingle();
            Container.BindInterfacesAndSelfTo<NodeBlocker<VertexData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<NodeViewSelector<VertexView>>().AsSingle();
            Container.BindInterfacesAndSelfTo<NodeViewMover<VertexView>>().AsSingle();
        }

        private void BindPathfinding()
        {
            Container.BindInterfacesAndSelfTo<AStarSearchAlgorithm<VertexData, LinkData<int>, int>>().AsSingle();
            Container.BindInterfacesAndSelfTo<VertexesHeuristicsProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<EuclideanDistance>().AsSingle();
            Container.BindInterfacesAndSelfTo<DistanceCostProvider<VertexData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<PathSetter<VertexData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<PathFinder<VertexData, int>>().AsSingle();
            Container.BindInterfacesAndSelfTo<LinePathDrawer>().AsSingle();
            Container.Bind<LineRenderer>().WithId(LinePathDrawer.LineRendererId).FromInstance(_pathLineRenderer).AsSingle();
        }

        private void BindSaveSystem()
        {
            Container.BindInterfacesAndSelfTo<Saver>().AsSingle();
            Container.BindInterfacesAndSelfTo<Loader>().AsSingle();

            //Choose only one of two variants (bytes or string)
            UseStringSaving();
            //UseBytesSaving();

            //Choose only one
            //Container.BindInterfacesAndSelfTo<DialogueFilePathProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<ConstantFilePathProvider>().AsSingle().WithArguments("Map.json", Environment.SpecialFolder.Desktop);

            Container.BindInterfacesAndSelfTo<VertexDataMapper>().AsSingle();
            Container.BindInterfacesAndSelfTo<LinkDataMapper>().AsSingle();
            Container.BindInterfacesAndSelfTo<VertexesFieldSaveDtoProvider>().AsSingle();


            #pragma warning disable CS8321
            void UseStringSaving()
            {
                Container.BindInterfacesAndSelfTo<StringDtoFileIO>().AsSingle();

                //Choose only one
                //Container.BindInterfacesAndSelfTo<NewtonsoftJsonStringSerializer>().AsSingle();
                Container.BindInterfacesAndSelfTo<UnityJsonStringSerializer>().AsSingle();
            }
            
            void UseBytesSaving()
            {
                Container.BindInterfacesAndSelfTo<BytesDtoFileIO>().AsSingle();

                //Choose only one
                //Container.BindInterfacesAndSelfTo<NewtonsoftJsonBytesSerializer>().AsSingle();
                //Container.BindInterfacesAndSelfTo<UnityJsonBytesSerializer>().AsSingle();
                Container.BindInterfacesAndSelfTo<GZipCompressedBytesSerializer>().FromSubContainerResolve()
                    .ByMethod(subContainer =>
                    {
                        subContainer.Bind<GZipCompressedBytesSerializer>().AsSingle();

                        //Choose only one
                        //subContainer.BindInterfacesAndSelfTo<NewtonsoftJsonBytesSerializer>().AsSingle();
                        subContainer.BindInterfacesAndSelfTo<UnityJsonBytesSerializer>().AsSingle();                        
                    }).AsSingle();
            }
            #pragma warning restore CS8321
        }

        private void BindUI()
        {
            //todo
            //Container.BindInterfacesAndSelfTo<UIHotkeyInfoPanel_Vertexes>().FromInstance(_hotkeyInfoPanel).AsSingle();
            Container.BindInterfacesAndSelfTo<UISaveLoadPanel>().FromInstance(_saveLoadPanel).AsSingle();
        }
    }
}