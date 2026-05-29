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

        public bool MoveNode(IMovableNode node, Vector2 nodePosition)
        {
            if (IsInsideBorders(nodePosition))
            {
                node.Move(nodePosition);
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