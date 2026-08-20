using EasyField.Nodes;
using EasyField.ObjectsStorages;
using EasyField.PathDrawers;
using EasyField.PathFinders;
using EasyField.PathSetters;
using System.Collections.Generic;

namespace EasyField.SceneControllers
{
    public class PathfindRunner<TNodeData, TNodeView, TId>
        where TNodeData : class, INodeData<TId>
        where TNodeView : class, INodeView<TId>
    {
        private readonly List<TNodeView> _nodeViewsPath = new();

        private readonly PathSetter<TNodeData> _pathSetter;
        private readonly PathFinder<TNodeData> _pathFinder;
        private readonly IPathDrawer<TNodeView> _pathDrawer;
        private readonly IObjectsStorage<TNodeView, TId> _nodeViews;


        public PathfindRunner(PathSetter<TNodeData> pathSetter, PathFinder<TNodeData> pathFinder, IPathDrawer<TNodeView> pathDrawer, 
            IObjectsStorage<TNodeView, TId> nodeViews)
        {
            _pathSetter = pathSetter;
            _pathFinder = pathFinder;
            _pathDrawer = pathDrawer;
            _nodeViews = nodeViews;
        }

        public void ProcessChanges(bool isReady)
        {
            _pathDrawer.ShowPath(false);

            if (!isReady)
                _pathDrawer.SetPath(null);
            else
                Run();
        }        

        private void Run()
        {
            var nodesPath = _pathFinder.GetPath(_pathSetter.StartNode, _pathSetter.FinishNode);
            if (nodesPath != null)
            {
                NodesToViews(nodesPath, _nodeViewsPath);
                _pathDrawer.SetPath(_nodeViewsPath);
                _pathDrawer.ShowPath(true);
            }
        }

        private void NodesToViews(IList<TNodeData> nodesList, IList<TNodeView> outViewsList)
        {
            outViewsList.Clear();

            for (int i = 0; i < nodesList.Count; i++)
            {
                outViewsList.Add(_nodeViews.GetItem(nodesList[i].Id));
            }
        }
    }
}