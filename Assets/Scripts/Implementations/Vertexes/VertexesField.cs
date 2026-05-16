using Core.Fields;
using Core.Implementations.Cells;
using Core.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core.Implementations.Vertexes
{
    public class VertexesField : MonoBehaviour, IPointerDownHandler, IGraph<VertexNode, int>
    {
        [SerializeField]
        protected BoxCollider2D _collider;
        
        protected VertexView _viewPrefab;

        protected Dictionary<int, VertexNode> _nodes = new Dictionary<int, VertexNode>();
        protected Dictionary<int, VertexView> _views = new Dictionary<int, VertexView>();

        private VertexesFieldGenerator _generator;
        private IInputService _inputService;

        public event Action<VertexNode, PointerEventData.InputButton, InputSnapshot> NodeClicked;
        public event Action<VertexesField, PointerEventData.InputButton, InputSnapshot> FieldClicked;


        /*
        [Inject]
        public void Construct(VertexView vertexViewPrefab)
        {
            _viewPrefab = vertexViewPrefab;
        }*/
        [Inject]
        public void Construct(VertexesFieldGenerator generator, IInputService inputService)
        {
            _inputService = inputService;
            _generator = generator;
        }

        private void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            //
            _generator.Test(this, transform);
        }

        public void AddFieldData(VertexNode node, VertexView view)
        {
            _nodes.Add(node.Id, node);
            _views.Add(view.Id, view);
        }

        public VertexNode GetNodeById(int id)
        {
            if (_nodes.ContainsKey(id))
                return _nodes[id];

            return null;
        }

        public VertexView GetViewForNode(VertexNode node)
        {
            if (_views.ContainsKey(node.Id))
                return _views[node.Id];

            return null;
        }

        public IReadOnlyList<VertexView> GetViewsForNodes(IList<VertexNode> nodePath) => nodePath.Select(GetViewForNode).ToList();

        public void OnPointerDown(PointerEventData eventData)
        {
            FieldClicked?.Invoke(this, eventData.button, _inputService.CreateSnapshot());
        }
    }
}