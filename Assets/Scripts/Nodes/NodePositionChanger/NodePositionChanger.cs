using System;
using UnityEngine;

namespace EasyField.Nodes.NodePositionChanger
{
    public class NodePositionChanger<TNodeData> : INodePositionChanger<TNodeData>
        where TNodeData : INodeData
    {
        public event Action<TNodeData, Vector2> NodePositionChanged;

        public bool TryChangeNodePosition(TNodeData nodeData, Vector2 pos)
        {
            if (nodeData == null)
                return false;

            if (nodeData.TryChangeNodePosition(pos))
            {
                NodePositionChanged?.Invoke(nodeData, nodeData.NodePosition);
                return true;
            }

            return false;
        }
    }
}