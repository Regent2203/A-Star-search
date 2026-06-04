using ThisProject.Nodes;
using UnityEngine;

namespace ThisProject.Implementations.Vertexes
{
    public class VertexNode : Node<int>
    {
        public VertexNode(int id, Vector2 nodePosition)
        {
            _id = id;
            _nodePosition = nodePosition;
        }
    }
}