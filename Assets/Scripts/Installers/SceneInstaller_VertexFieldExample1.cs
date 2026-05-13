using Core.Links.Factories.CostProviders;
using Core.Heuristic.Functions;
using Core.Implementations.Cells;
using Core.Implementations.Vertexes;
using Core.Links.Factories;
using Core.Links.Providers;
using Core.PathDrawers;
using Core.PathFinders;
using Core.SearchAlgorithms;
using UnityEngine;
using Zenject;

namespace Core.Installers
{
    public class SceneInstaller_VertexFieldExample1 : MonoInstaller
    {
        [SerializeField]
        private VertexesField _field;
        [SerializeField]
        private VertexView _vertexViewPrefab;
        //[SerializeField]
        //private UIHotkeyInfoPanel_Vertexes _hotkeyInfoPanel;
        [SerializeField]
        private LineRenderer _pathLineRenderer;
        [SerializeField]
        private KeyCode _creatingKeyCode = KeyCode.LeftControl;
        [SerializeField]
        private KeyCode _linkingKeyCode = KeyCode.LeftAlt;
        [SerializeField]
        private KeyCode _markingKeyCode = KeyCode.LeftShift;

        public override void InstallBindings()
        {
            Container.BindInstance(_vertexViewPrefab).AsSingle();

            Container.BindInterfacesAndSelfTo<VertexesField>().FromInstance(_field).AsSingle();
            Container.BindInterfacesAndSelfTo<StoredLinksProvider<VertexNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<LinksFactory<VertexNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<AStarSearchAlgorithm<VertexNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<VertexesHeuristicsProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<EuclideanDistance>().AsSingle();
            Container.BindInterfacesAndSelfTo<ConstantCostProvider<VertexNode>>().AsSingle().WithArguments(0.0f);
            Container.BindInterfacesAndSelfTo<PathFinder<VertexNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<LinePathDrawer>().AsSingle();
            Container.BindInterfacesAndSelfTo<PathSetter<VertexNode>>().AsSingle(); 
            //Container.BindInterfacesAndSelfTo<UIHotkeyInfoPanel_Vertexes>().FromInstance(_hotkeyInfoPanel).AsSingle();
            Container.BindInterfacesAndSelfTo<LineRenderer>().FromInstance(_pathLineRenderer).AsSingle();
            Container.BindInstance(_creatingKeyCode).WithId("CreatingKey");
            Container.BindInstance(_linkingKeyCode).WithId("LinkingKey");
            Container.BindInstance(_markingKeyCode).WithId("MarkingKey");
        }
    }
}