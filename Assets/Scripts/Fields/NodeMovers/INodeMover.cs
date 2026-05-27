using ThisProject.Nodes;
using UnityEngine;

namespace ThisProject.Fields.NodeMovers
{
    public interface INodeMover
    {
        public bool MoveNode(IMovableNode node, Vector2 nodePosition);
    }
}