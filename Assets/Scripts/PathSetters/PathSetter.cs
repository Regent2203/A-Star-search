using System;
using ThisProject.Nodes;

namespace ThisProject.PathSetters
{
    public class PathSetter<T> : IPathSetter<T> where T : class, INode
    {
        private T _startNode;
        private T _finishNode;

        public T StartNode => _startNode;
        public T FinishNode => _finishNode;

        public event Action<bool> AnyNodeChanged;
        public event Action<T, bool> StartNodeChanged;  //false is called when cleared, true is called when assigned
        public event Action<T, bool> FinishNodeChanged; //false is called when cleared, true is called when assigned
        public bool IsReady => _startNode != null && _finishNode != null;


        public void UpdateStartNode(T node)
        {
            UpdateDesiredNode(node, ref _startNode, ref _finishNode, StartNodeChanged);
        }

        public void UpdateFinishNode(T node)
        {
            UpdateDesiredNode(node, ref _finishNode, ref _startNode, FinishNodeChanged);
        }

        private void UpdateDesiredNode(T node, ref T desiredNode, ref T notDesiredNode, Action<T, bool> desiredNodeChanged)
        {
            if (node is not null && ReferenceEquals(notDesiredNode, node)) //when trying to set start node as finish node or vice versa, we do nothing (it's a feature)
                return;

            if (node is null && desiredNode is null) //when trying to set null to null
                return;

            if (ReferenceEquals(desiredNode, node)) //when trying to set same node value to desired node, we clear desired value instead (it's a feature)
            {
                var oldDesiredNode = desiredNode;
                desiredNode = null;
                desiredNodeChanged?.Invoke(oldDesiredNode, false);
                AnyNodeChanged?.Invoke(IsReady);
                return;
            }

            if (desiredNode is not null) //if desired node is already set, we should clear the previous one
            {
                desiredNodeChanged?.Invoke(desiredNode, false);
            }
            desiredNode = node;
            desiredNodeChanged?.Invoke(desiredNode, true);

            AnyNodeChanged?.Invoke(IsReady);
        }
    }
}