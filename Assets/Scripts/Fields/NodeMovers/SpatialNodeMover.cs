using System;
using ThisProject.Nodes;
using UnityEngine;

namespace ThisProject.Fields.NodeMovers
{
    public class SpatialNodeMover : INodeMover
    {
        private readonly BoxCollider2D _box;

        public event Action<IMovableNode, Vector2> NodeMoved;


        public SpatialNodeMover(IVisibleField field) 
        {
            _box = field.Box;
        }

        public bool TryMoveNode(IMovableNode node, Vector2 nodePosition)
        {
            if (node == null)
                return false;

            if (!IsInsideBorders(nodePosition))
                return false;
            
            if (node.TryMove(nodePosition))
            {
                NodeMoved?.Invoke(node, nodePosition);
                return true;
            }
            return false;
        }

        private bool IsInsideBorders(Vector2 nodePosition)
        {
            return _box.OverlapPoint(nodePosition);
        }
    }
}