using System;
using UnityEngine;

namespace ThisProject.Nodes.ViewSelectors
{
    public class NodeViewSelector<V> : INodeViewSelector<V>
        where V : MonoBehaviour, INodeView
    {
        private V _selectedView;

        public V SelectedView => _selectedView;

        public event Action<V, bool> ViewSelected; //true when select, false when deselect


        public void SelectView(V view)
        {
            if (_selectedView == view)
                return;

            if (_selectedView != null)
            {
                ViewSelected?.Invoke(_selectedView, false);
            }

            _selectedView = view;

            if (view != null)
            {
                ViewSelected?.Invoke(_selectedView, true);
            }
        }
    }
}
