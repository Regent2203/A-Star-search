using System;
using System.Collections.Generic;

namespace EasyField.Nodes.ViewSelectors
{
    public class NodeViewSelector<TNodeView> : INodeViewSelector<TNodeView>
        where TNodeView : INodeView
    {
        private TNodeView _selectedNodeView;

        public TNodeView SelectedNodeView => _selectedNodeView;

        public event Action<TNodeView, bool> NodeViewSelected; //true when select, false when deselect


        public void SelectView(TNodeView nodeView)
        {
            if (EqualityComparer<TNodeView>.Default.Equals(_selectedNodeView, nodeView))
                return;

            if (_selectedNodeView != null)
            {
                NodeViewSelected?.Invoke(_selectedNodeView, false);
            }

            _selectedNodeView = nodeView;

            if (nodeView != null)
            {
                NodeViewSelected?.Invoke(_selectedNodeView, true);
            }
        }
    }
}
