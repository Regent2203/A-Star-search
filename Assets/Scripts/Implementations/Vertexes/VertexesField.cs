using Core.Fields;
using Core.Fields.Spatials;
using Core.Implementations.Cells;
using Core.Inputs;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core.Implementations.Vertexes
{
    public class VertexesField : SpatialField<VertexNode, VertexView>
    {
        private VertexesFieldGenerator _generator;

        public event Action<VertexesField, PointerEventData.InputButton, InputSnapshot> FieldClicked;
        public event Action<int, Vector2> NodeDragBegin;
        public event Action<int, Vector2> NodeDragEnd;


        [Inject]
        public void Construct(FieldClickHandler<VertexNode, VertexView, int> clickHandler, VertexesFieldGenerator generator)
        {
            _clickHandler = clickHandler;
            _generator = generator;
        }

        private void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            base.Init();

            _clickHandler.SetConfiguration(Nodes, _views, NotifyNodeClicked);
            //todo
            _generator.SetConfiguration(this, transform, NotifyNodeDragBegin, NotifyNodeDragEnd);
            _generator.TestPopulate();
        }

        public void AddFieldData(VertexNode node, VertexView view)
        {
            //_nodes.Add(node.Id, node);
            //_views.Add(view.Id, view);
        }

        private void NotifyNodeDragBegin(int id, Vector2 oldNodePosition)
        {
            NodeDragBegin?.Invoke(id, oldNodePosition);
        }
        private void NotifyNodeDragEnd(int id, Vector2 newNodePosition)
        {
            NodeDragEnd?.Invoke(id, newNodePosition);
            Nodes.GetById(id).Move(newNodePosition);
        }
    }
}