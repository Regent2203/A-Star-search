using EasyField.ObjectsStorages;
using EasyField.PathDrawers;
using EasyField.PathFinders;
using EasyField.PathSetters;
using EasyField.SceneControllers;

namespace EasyField.Implementations.Vertexes
{
    public class VertexesPathfindRunner : PathfindRunner<VertexData, VertexView, int>
    {
        public VertexesPathfindRunner(PathSetter<VertexData> pathSetter, PathFinder<VertexData> pathFinder, IPathDrawer<VertexView> pathDrawer, 
            IObjectsStorage<VertexView, int> nodeViews) : base(pathSetter, pathFinder, pathDrawer, nodeViews)
        {
        }
    }
}