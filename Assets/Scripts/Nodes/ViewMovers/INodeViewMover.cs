using System;
using UnityEngine;

namespace EasyField.Nodes.ViewMovers
{
    public interface INodeViewMover<TNodeView>
        where TNodeView : INodeView
    {
        public bool TryMoveView(TNodeView nodeView, ref Vector2 position);

        public event Action<TNodeView, Vector2> NodeViewMoved;
    }
}