using System;

namespace EasyField.Nodes.NodeBlockers
{
    public interface INodeBlocker<TNodeData>
        where TNodeData : INodeData
    {
        public bool TryBlockNode(TNodeData nodeData, bool block);

        public event Action<TNodeData, bool> NodeBlocked;
    }
}