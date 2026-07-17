using System;
using System.ComponentModel;
using System.Reflection;
using ThisProject.Fields;
using ThisProject.Heuristic.Functions;
using ThisProject.Implementations.Cells;
using ThisProject.Implementations.Cells.UI;
using ThisProject.Implementations.Vertexes;
using ThisProject.Implementations.VisualLinks;
using ThisProject.Inputs;
using ThisProject.Links;
using ThisProject.Links.Factories;
using ThisProject.Links.Factories.CostProviders;
using ThisProject.Links.Providers;
using ThisProject.Nodes.NodeBlockers;
using ThisProject.Nodes.ViewMovers;
using ThisProject.Nodes.ViewSelectors;
using ThisProject.ObjectsStorages;
using ThisProject.PathDrawers;
using ThisProject.PathFinders;
using ThisProject.PathSetters;
using ThisProject.SaveSystem;
using ThisProject.SaveSystem.Dto;
using ThisProject.SaveSystem.DtoFileIOs;
using ThisProject.SaveSystem.FilePathProviders;
using ThisProject.SaveSystem.Mappers;
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
        private VisualLink<VertexData> _visualLinkPrefab;
        [SerializeField]
        private VertexesVisualLinksCreator _visualLinksManager;
        [SerializeField]
        private LineRenderer _pathLineRenderer;
        [SerializeField]
        private UISaveLoadPanel _saveLoadPanel;


        public override void InstallBindings()
        {
            BindStarter();
            BindEnviroment();
            BindField();
            BindPathfinding();
            BindLinks();
            BindManipulators();
            BindSaveSystem();
            BindUI();
        }

        private void BindStarter()
        {
            Container.BindInterfacesAndSelfTo<Starter_Scene2a>().AsSingle();
        }

        private void BindEnviroment()
        {
            Container.BindInstance(_mainCamera).AsSingle();
            Container.BindInstance(_inputSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<UnityInputService>().AsSingle();
        }

        private void BindField()
        {
            Container.BindInterfacesAndSelfTo<SpatialField>().FromInstance(_field).AsSingle();
            Container.Bind(typeof(VertexDataStorage), typeof(DictTypeStorage<VertexData, int>), typeof(IObjectsStorage<VertexData, int>)).
                To<VertexDataStorage>().AsSingle();
            Container.Bind(typeof(VertexViewStorage), typeof(DictTypeStorage<VertexView, int>), typeof(IObjectsStorage<VertexView, int>)).
                To<VertexViewStorage>().AsSingle();
            Container.Bind(typeof(DictTypeStorage<LinkData<VertexData>, int>), typeof(IObjectsStorage<LinkData<VertexData>, int>)).
                To<DictTypeStorage<LinkData<VertexData>, int>>().AsSingle(); //todo

            Container.BindInterfacesAndSelfTo<VertexesFieldBuilder>().AsSingle();
            Container.BindMemoryPool<VertexData, VertexDataPool>().WithInitialSize(20);
            Container.BindMemoryPool<VertexView, VertexViewPool>().WithInitialSize(20).
                FromComponentInNewPrefab(_vertexViewPrefab).UnderTransform(_field.NodesContainer);

            //Container.BindInterfacesAndSelfTo<VertexView>().FromInstance(_vertexViewPrefab).AsSingle();
        }

        private void BindLinks()
        {
            Container.BindInstance(_visualLinkPrefab).AsSingle();
            Container.BindInterfacesAndSelfTo<VertexesVisualLinksCreator>().FromInstance(_visualLinksManager).AsSingle();
            Container.BindInterfacesAndSelfTo<VisualLinksFactory<VertexData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<VisualLinksPool<VertexData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<StoredLinksProvider<VertexData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<LinksFactory<VertexData>>().AsSingle();
        }

        private void BindManipulators()
        {
            Container.BindInstance(_clickHandler).AsSingle();
            Container.BindInstance(_dragHandler).AsSingle();
            Container.BindInterfacesAndSelfTo<NodeBlocker<VertexData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<NodeViewSelector<VertexView>>().AsSingle();
            Container.BindInterfacesAndSelfTo<NodeViewMover>().AsSingle();
        }

        private void BindPathfinding()
        {
            Container.BindInterfacesAndSelfTo<AStarSearchAlgorithm<VertexData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<VertexesHeuristicsProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<EuclideanDistance>().AsSingle();
            Container.BindInterfacesAndSelfTo<ConstantCostProvider<VertexData>>().AsSingle().WithArguments(0.0f);
            Container.BindInterfacesAndSelfTo<PathSetter<VertexData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<PathFinder<VertexData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<LinePathDrawer>().AsSingle();
            Container.Bind<LineRenderer>().WithId(LinePathDrawer.LineRendererId).FromInstance(_pathLineRenderer).AsSingle();
        }

        private void BindSaveSystem()
        {
            Container.BindInterfacesAndSelfTo<Saver>().AsSingle();
            Container.BindInterfacesAndSelfTo<Loader>().AsSingle();

            //Choose only one of two variants (binary or string)
            UseStringSaving();
            //UseBinarySaving();

            //Choose only one
            //Container.BindInterfacesAndSelfTo<DialogueFilePathProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<ConstantFilePathProvider>().AsSingle().WithArguments("Map.json", Environment.SpecialFolder.Desktop);

            Container.BindInterfacesAndSelfTo<VertexDataMapper>().AsSingle();
            Container.BindInterfacesAndSelfTo<VertexesFieldSaveDtoProvider>().AsSingle();


            #pragma warning disable CS8321
            void UseStringSaving()
            {
                Container.BindInterfacesAndSelfTo<StringDtoFileIO>().AsSingle();

                //Choose only one
                Container.BindInterfacesAndSelfTo<NewtonsoftJsonStringSerializer>().AsSingle();
                //Container.BindInterfacesAndSelfTo<UnityJsonStringSerializer>().AsSingle();
            }
            
            void UseBinarySaving()
            {
                Container.BindInterfacesAndSelfTo<BinaryDtoFileIO>().AsSingle();

                //Choose only one
                //Container.BindInterfacesAndSelfTo<NewtonsoftJsonBinarySerializer>().AsSingle();
                Container.BindInterfacesAndSelfTo<UnityJsonBinarySerializer>().AsSingle();
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