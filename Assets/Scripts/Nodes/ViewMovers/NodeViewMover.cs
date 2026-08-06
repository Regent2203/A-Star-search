using EasyField.Fields;
using System;
using UnityEngine;

namespace EasyField.Nodes.ViewMovers
{
    public class NodeViewMover<TNodeView> : INodeViewMover<TNodeView>
        where TNodeView : INodeView
    {
        private readonly IField _field;

        public event Action<TNodeView, Vector2> NodeViewMoved;


        public NodeViewMover(IField field) 
        {
            _field = field;
        }

        public bool TryMoveView(TNodeView nodeView, ref Vector2 position)
        {
            if (nodeView == null)
                return false;

            var offset = nodeView.GetSize() / 2;
            position = position.Clamp(_field.Box.bounds, offset);

            nodeView.Move(position);
            NodeViewMoved?.Invoke(nodeView, position);

            return true;
        }
    }
}