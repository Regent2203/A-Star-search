using Core.Nodes;
using UnityEngine.EventSystems;

namespace Core.PathFinders
{
    public class PathSetter<T> where T : class, INode
    {
        private readonly IPathFinder<T> _pathFinder;
        

        public PathSetter(IPathFinder<T> pathFinder)
        {
            _pathFinder = pathFinder;
        }

        //todo: remove, also add enum to painter (left-right)
        public void TryUseNode(T node, PointerEventData.InputButton btn)
        {            
            if (btn == PointerEventData.InputButton.Left) //lmb
            {
                _pathFinder.UpdateStartNode(node);
            }
            else if (btn == PointerEventData.InputButton.Right) //rmb
            {
                _pathFinder.UpdateFinishNode(node);
            }
        }
    }
}