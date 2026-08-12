using System;
using UnityEngine;

namespace EasyField.Nodes.NodePositionChanger
{
    public interface INodePositionChanger<TNodeData>
        where TNodeData : INodeData
    {
        public bool TryChangeNodePosition(TNodeData nodeData, Vector2 pos);

        public event Action<TNodeData, Vector2> NodePositionChanged;
    }
}