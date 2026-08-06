using System;

namespace EasyField.Nodes.ViewSelectors
{
    public interface INodeViewSelector<TNodeView>
        where TNodeView : INodeView
    {
        public TNodeView SelectedNodeView { get; }

        public void SelectView(TNodeView nodeView);

        public event Action<TNodeView, bool> NodeViewSelected;
    }
}
