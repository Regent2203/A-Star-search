using Core.Fields;
using Core.Fields.Spatials;
using Core.Implementations.Cells;
using Core.Inputs;
using Core.ObjectsStorages;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core.Implementations.Vertexes
{
    public class VertexesField : SpatialField<VertexNode, VertexView>
    {
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
        public void Construct(FieldClickHandler<VertexNode, VertexView, int> clickHandler, VertexesFieldGenerator generator, IInputService inputService)
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
            Nodes.GetById(id).Move(newNodePosition);
        }
    }
}