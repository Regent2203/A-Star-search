using System;
using UnityEngine;
using Zenject;

namespace Core.Implementations.Vertexes
{
    public class VertexNodeFactory
    {
        //private Action<CellNode, CellType> _typeChangedCallback;

        private readonly IInstantiator _instantiator;


        public VertexNodeFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }
        /*
        public void SetConfiguration(Action<CellNode, CellType> typeChangedCallback)
        {
            _typeChangedCallback = typeChangedCallback;
        }
        */
        public VertexNode Create(Vector2 nodePosition, int id)
        {
            var vertexNode = _instantiator.Instantiate<VertexNode>(new object[] { nodePosition, id });
            //var vertexNode = _instantiator.Instantiate<VertexNode>(new object[] { nodePosition, index, cellType, _typeChangedCallback });

            return vertexNode;
        }
    }
}