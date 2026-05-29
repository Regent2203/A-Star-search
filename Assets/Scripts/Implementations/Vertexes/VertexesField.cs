using System;
using ThisProject.Fields;
using ThisProject.Implementations.Cells;
using UnityEngine;
using Zenject;

namespace ThisProject.Implementations.Vertexes
{
    public class VertexesField : SpatialField<VertexNode, VertexView>
    {
        private VertexesFieldGenerator _generator;

        public event Action<int, Vector2> NodeDragBegin;
        public event Action<int, Vector2> NodeDragEnd;


        [Inject]
        public void Construct(VertexesFieldGenerator generator)
        {
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