using Core.Fields;
using Core.HeuristicFunctions;
using Core.SearchAlgorithms;
using UnityEngine;
using Zenject;

namespace Core.Installers
{
    public class SceneInstaller_CellGrid : MonoInstaller
    {
        [SerializeField]
        private CellGridField _field;

        public override void InstallBindings()
        {
            /*
            Container.Bind<AbstractField>().FromInstance(_field).AsSingle();
            Container.Bind<IHeuristicFunction>().To<ManhattanDistance>().AsSingle();
            Container.Bind<ISearchAlgorithm>().To<AStarSearchAlgorithm>().AsSingle();
            */
        }
    }
}