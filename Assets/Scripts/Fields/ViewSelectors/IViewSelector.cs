using System;
using ThisProject.Views;
using UnityEngine;

namespace ThisProject.Fields.ViewSelectors
{
    public interface IViewSelector<V>
        where V : MonoBehaviour, IView
    {
        public V SelectedView { get; }

        public void SelectView(V view);

        public event Action<V, bool> ViewSelected;
    }
}
