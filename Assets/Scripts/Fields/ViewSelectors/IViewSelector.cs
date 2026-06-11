using System;
using ThisProject.Views;
using UnityEngine;

namespace ThisProject.Fields.ViewMovers
{
    public interface IViewSelector<V>
        where V : MonoBehaviour, IView
    {
        public V SelectedView { get; }

        public void SelectView(V view, bool clearSame);

        public event Action<V, bool> ViewSelected;
    }
}
