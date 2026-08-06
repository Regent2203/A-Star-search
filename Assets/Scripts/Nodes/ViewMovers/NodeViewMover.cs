using System;
using EasyField.Fields;
using UnityEngine;

namespace EasyField.Nodes.ViewMovers
{
    public class NodeViewMover<V> : INodeViewMover<V>
        where V : MonoBehaviour, INodeView
    {
        private readonly IField _field;

        public event Action<V, Vector2> ViewMoved;


        public NodeViewMover(IField field) 
        {
            _field = field;
        }

        public bool TryMoveView(V view, ref Vector2 position)
        {
            if (view == null)
                return false;

            var offset = view.GetSize() / 2;
            position = position.Clamp(_field.Box.bounds, offset);

            view.Move(position);
            ViewMoved?.Invoke(view, position);

            return true;
        }
    }
}