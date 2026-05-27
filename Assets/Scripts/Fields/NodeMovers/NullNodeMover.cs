using ThisProject.Nodes;
using UnityEngine;

namespace ThisProject.Fields.NodeMovers
{
    public class NullNodeMover : INodeMover
    {
        public bool MoveNode(IMovableNode node, Vector2 nodePosition)
        {
            return false;
        }
    }
}