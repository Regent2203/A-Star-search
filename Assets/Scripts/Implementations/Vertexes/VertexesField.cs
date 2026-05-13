using Core.Fields;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Core.Implementations.Vertexes
{
    public class VertexesField : MonoBehaviour, IGraph<VertexNode, int>
    {
        [SerializeField]
        protected BoxCollider2D _collider;
        
        protected VertexView _viewPrefab;

        protected Dictionary<int, VertexNode> _nodes;
        protected Dictionary<int, VertexView> _views;

        /*
        [Inject]
        public void Construct(VertexView vertexViewPrefab)
        {
            _viewPrefab = vertexViewPrefab;
        }*/

        private void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            //
        }

        public VertexNode GetNodeById(int id)
        {
            if (_nodes.ContainsKey(id))
                return _nodes[id];

            return null;
        }
    }
}