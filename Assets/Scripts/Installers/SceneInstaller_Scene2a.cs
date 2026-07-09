using System.ComponentModel;
using ThisProject.Fields;
using ThisProject.Heuristic.Functions;
using ThisProject.Implementations.Cells;
using ThisProject.Implementations.Cells.UI;
using ThisProject.Implementations.Vertexes;
using ThisProject.Implementations.VisualLinks;
using ThisProject.Inputs;
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
            Container.BindInstance(_mainCamera).AsSingle();
            Container.BindInstance(_inputSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<UnityInputService>().AsSingle();
            Container.BindInterfacesAndSelfTo<VertexView>().FromInstance(_vertexViewPrefab).AsSingle();
            Container.BindInterfacesAndSelfTo<SpatialField>().FromInstance(_field).AsSingle();
            Container.Bind(typeof(VertexDataStorage), typeof(DictTypeStorage<VertexData, int>), typeof(IObjectsStorage<VertexData, int>)).To<VertexDataStorage>().AsSingle();
            Container.Bind(typeof(VertexViewStorage), typeof(DictTypeStorage<VertexView, int>), typeof(IObjectsStorage<VertexView, int>)).To<VertexViewStorage>().AsSingle();
            Container.BindInstance(_clickHandler).AsSingle();
            Container.BindInstance(_dragHandler).AsSingle();            
            Container.BindInterfacesAndSelfTo<VertexesFieldBuilder>().AsSingle();
            Container.BindMemoryPool<VertexData, VertexDataPool>().WithInitialSize(20);
            Container.BindMemoryPool<VertexView, VertexViewPool>().WithInitialSize(20).
                FromComponentInNewPrefab(_vertexViewPrefab).UnderTransform(_field.NodesContainer);
            Container.BindInterfacesAndSelfTo<NodeBlocker<VertexData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<NodeViewSelector<VertexView>>().AsSingle();
            Container.BindInterfacesAndSelfTo<NodeViewMover>().AsSingle();
            Container.BindInstance(_visualLinkPrefab).AsSingle();
            Container.BindInterfacesAndSelfTo<VertexesVisualLinksCreator>().FromInstance(_visualLinksManager).AsSingle();
            Container.BindInterfacesAndSelfTo<VisualLinksFactory<VertexData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<VisualLinksPool<VertexData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<StoredLinksProvider<VertexData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<LinksFactory<VertexData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<AStarSearchAlgorithm<VertexData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<VertexesHeuristicsProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<EuclideanDistance>().AsSingle();
            Container.BindInterfacesAndSelfTo<ConstantCostProvider<VertexData>>().AsSingle().WithArguments(0.0f);
            Container.BindInterfacesAndSelfTo<PathSetter<VertexData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<PathFinder<VertexData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<LinePathDrawer>().AsSingle();
            Container.BindInterfacesAndSelfTo<LineRenderer>().FromInstance(_pathLineRenderer).AsSingle();

            //savesystem
            Container.BindInterfacesAndSelfTo<TextSaver<VertexData, VertexDataDto, int>>().AsSingle();
            Container.BindInterfacesAndSelfTo<TextLoader<FieldSaveDto<VertexDataDto, int>>>().AsSingle();
            Container.BindInterfacesAndSelfTo<VertexDataMapper>().AsSingle();

            //Container.BindInterfacesAndSelfTo<NewtonsoftJsonTextSerializer>().AsSingle();
            Container.BindInterfacesAndSelfTo<UnityJsonTextSerializer>().AsSingle(); //alternative

            //Container.BindInterfacesAndSelfTo<DialogueFilePathProvider>().AsSingle(); //alternative
            Container.BindInterfacesAndSelfTo<ConstantFilePathProvider>().AsSingle().WithArguments("Map.json");

            //Container.BindInterfacesAndSelfTo<UIHotkeyInfoPanel_Vertexes>().FromInstance(_hotkeyInfoPanel).AsSingle();
            Container.BindInterfacesAndSelfTo<UISaveLoadPanel>().FromInstance(_saveLoadPanel).AsSingle();

            Container.BindInterfacesAndSelfTo<Starter_Scene2a>().AsSingle();
        }
    }
}