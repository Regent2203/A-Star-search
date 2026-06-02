using UnityEngine;
using Zenject;

namespace ThisProject.Implementations.Vertexes
{
    public class VertexNodeFactory
    {
        private readonly IInstantiator _instantiator;


        public VertexNodeFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }
        
        public VertexNode Create(int id, Vector2 nodePosition)
        {
            var vertexNode = _instantiator.Instantiate<VertexNode>(new object[] { id, nodePosition });

            return vertexNode;
        }
    }
}