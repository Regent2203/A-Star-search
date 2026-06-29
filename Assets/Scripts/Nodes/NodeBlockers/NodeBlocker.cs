using System;

namespace ThisProject.Nodes.NodeBlockers
{
    public class NodeBlocker<T> : INodeBlocker<T>
        where T : INodeData
    {
        public event Action<T, bool> NodeBlocked;

        public bool TryBlockNode(T node, bool block)
        {
            if (node == null) 
                return false;

            if (node.TrySetBlocked(block))
            {
                NodeBlocked?.Invoke(node, node.IsBlocked);
                return true;
            }

            return false;
        }
    }
}