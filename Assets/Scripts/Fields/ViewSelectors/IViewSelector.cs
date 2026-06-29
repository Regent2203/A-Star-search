using System;
using ThisProject.Nodes;
using UnityEngine;

namespace ThisProject.Fields.ViewSelectors
{
    public interface IViewSelector<V>
        where V : MonoBehaviour, INodeView
    {
        public V SelectedView { get; }

        public void SelectView(V view);

        public event Action<V, bool> ViewSelected;
    }
}
