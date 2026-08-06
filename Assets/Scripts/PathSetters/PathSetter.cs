using System;
using EasyField.Nodes;

namespace EasyField.PathSetters
{
    public class PathSetter<TNodeData> : IPathSetter<TNodeData>
        where TNodeData : INodeData
    {
        private TNodeData _startNode;
        private TNodeData _finishNode;

        public TNodeData StartNode => _startNode;
        public TNodeData FinishNode => _finishNode;

        public event Action<bool> AnyNodeChanged;
        public event Action<TNodeData, bool> StartNodeChanged;  //false is called when cleared, true is called when assigned
        public event Action<TNodeData, bool> FinishNodeChanged; //false is called when cleared, true is called when assigned
        public bool IsReady => _startNode != null && _finishNode != null;


        public void UpdateStartNode(TNodeData node)
        {
            UpdateDesiredNode(node, ref _startNode, ref _finishNode, StartNodeChanged);
        }

        public void UpdateFinishNode(TNodeData node)
        {
            UpdateDesiredNode(node, ref _finishNode, ref _startNode, FinishNodeChanged);
        }

        private void UpdateDesiredNode(TNodeData node, ref TNodeData desiredNode, ref TNodeData notDesiredNode, Action<TNodeData, bool> desiredNodeChanged)
        {
            if (node is not null && ReferenceEquals(notDesiredNode, node)) //when trying to set start node as finish node or vice versa, we do nothing (it's a feature)
                return;

            if (node is null && desiredNode is null) //when trying to set null to null
                return;

            if (ReferenceEquals(desiredNode, node)) //when trying to set same node value to desired node, we clear desired value instead (it's a feature)
            {
                var oldDesiredNode = desiredNode;
                desiredNode = default;
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