using System;

namespace ThisProject.Nodes.NodeBlockers
{
    public interface INodeBlocker<T>
        where T : INodeData
    {
        public bool TryBlockNode(T node, bool block);

        public event Action<T, bool> NodeBlocked;
    }
}