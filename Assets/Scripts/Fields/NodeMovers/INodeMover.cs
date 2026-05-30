using System;
using ThisProject.Nodes;
using UnityEngine;

namespace ThisProject.Fields.NodeMovers
{
    public interface INodeMover
    {
        public bool TryMoveNode(IMovableNode node, Vector2 nodePosition);

        public event Action<IMovableNode, Vector2> NodeMoved;
    }
}