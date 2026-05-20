using Core.Fields;
using Core.Fields.Grids;
using Core.Implementations.Cells;
using Core.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
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

        protected FieldClickHandler<VertexNode> _clickHandler; //todo

        private VertexesFieldGenerator _generator;
        private IInputService _inputService;

        public event Action<VertexesField, PointerEventData.InputButton, InputSnapshot> FieldClicked;
        public event Action<VertexNode, PointerEventData.InputButton, InputSnapshot> NodeClicked;
        public event Action<int, Vector2> NodeDragBegin;
        public event Action<int, Vector2> NodeDragEnd;


        /*
        [Inject]
        public void Construct(VertexView vertexViewPrefab)
        {
            _viewPrefab = vertexViewPrefab;
        }*/
        [Inject]
        public void Construct(FieldClickHandler<VertexNode> clickHandler, VertexesFieldGenerator generator, IInputService inputService)
        {
            _clickHandler = clickHandler;
            _inputService = inputService;
            _generator = generator;
        }

        private void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            _clickHandler.SetConfiguration(this, NotifyNodeClicked);
            //todo
            _generator.SetConfiguration(this, transform, NotifyNodeDragBegin, NotifyNodeDragEnd);
            _generator.TestPopulate();
        }

        public void AddFieldData(VertexNode node, VertexView view)
        {
            _nodes.Add(node.Id, node);
            _views.Add(view.Id, view);
        }

        public VertexNode GetNodeById(int id)
        {
            if (_nodes.TryGetValue(id, out var node)) 
                return node;

            return null;
        }

        public VertexView GetViewForNode(VertexNode node)
        {
            if (_views.TryGetValue(node.Id, out var view))
                return view;

            return null;
        }

        public IReadOnlyList<VertexView> GetViewsForNodes(IList<VertexNode> nodePath) => nodePath.Select(GetViewForNode).ToList();

        //input handler
        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_clickHandler.ProcessClick(eventData))
                ;
            //FieldClicked?.Invoke(this, eventData.button, _inputService.CreateSnapshot()); //todo
        }

        //
        private void NotifyNodeClicked(VertexNode node, PointerEventData.InputButton btn, InputSnapshot input)
        {
            NodeClicked?.Invoke(node, btn, input);
        }

        private void NotifyNodeDragBegin(int id, Vector2 oldNodePosition)
        {
            NodeDragBegin?.Invoke(id, oldNodePosition);
        }
        private void NotifyNodeDragEnd(int id, Vector2 newNodePosition)
        {
            NodeDragEnd?.Invoke(id, newNodePosition);
            GetNodeById(id).Move(newNodePosition);
        }
    }
}