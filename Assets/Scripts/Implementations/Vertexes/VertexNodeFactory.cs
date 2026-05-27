using System;
using UnityEngine;
using Zenject;

namespace ThisProject.Implementations.Vertexes
{
    public class VertexNodeFactory
    {
        private Action<int, Vector2> _nodePositionChangedCallback;

        private readonly IInstantiator _instantiator;


        public VertexNodeFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }
        
        public void SetConfiguration(Action<int, Vector2> nodePositionChangedCallback)
        {
            _nodePositionChangedCallback = nodePositionChangedCallback;
        }
        
        public VertexNode Create(int id, Vector2 nodePosition)
        {
            var vertexNode = _instantiator.Instantiate<VertexNode>(new object[] { id, nodePosition });
            //vertexNode.NodeMoved += (v) => _nodeMovedCallback(vertexNode.Id, v); //todo

            return vertexNode;
        }


    }
}