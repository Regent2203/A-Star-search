using System;
using UnityEngine;

namespace ThisProject.Nodes.ViewSelectors
{
    public interface INodeViewSelector<V>
        where V : MonoBehaviour, INodeView
    {
        public V SelectedView { get; }

        public void SelectView(V view);

        public event Action<V, bool> ViewSelected;
    }
}
