using System;

namespace EasyField.Nodes.NodeBlockers
{
    public class NodeBlocker<TNodeData> : INodeBlocker<TNodeData>
        where TNodeData : INodeData
    {
        public event Action<TNodeData, bool> NodeBlocked;

        public bool TryBlockNode(TNodeData nodeData, bool block)
        {
            if (nodeData == null) 
                return false;

            if (nodeData.TrySetBlocked(block))
            {
                NodeBlocked?.Invoke(nodeData, nodeData.IsBlocked);
                return true;
            }

            return false;
        }
    }
}