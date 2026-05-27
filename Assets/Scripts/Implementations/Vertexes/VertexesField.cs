using ThisProject.Fields;
using ThisProject.Fields.Implementations;
using ThisProject.Implementations.Cells;
using ThisProject.Inputs;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using ThisProject.Fields.ClickHandlers;
using ThisProject.Fields.NodeMovers;

namespace ThisProject.Implementations.Vertexes
{
    public class VertexesField : SpatialSceneField<VertexNode, VertexView>
    {
        private VertexesFieldGenerator _generator;
        private SpatialNodeMover _nodeMover;

        public override INodeMover NodeMover => _nodeMover;

        public event Action<int, Vector2> NodeDragBegin;
        public event Action<int, Vector2> NodeDragEnd;


        [Inject]
        public void Construct(SpatialClickHandler<VertexNode, VertexView, int> clickHandler, VertexesFieldGenerator generator)
        {
            _clickHandler = clickHandler;
            _generator = generator;
        }

        protected override void Init()
        {
            base.Init();
            
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
            GetNodeById(id).Move(newNodePosition);
        }
    }
}