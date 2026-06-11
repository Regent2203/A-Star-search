using System;
using ThisProject.Views;
using UnityEngine;

namespace ThisProject.Fields.ViewMovers
{
    public class ViewSelector<V> : IViewSelector<V>
        where V : MonoBehaviour, IView
    {
        private V _selectedView;

        public V SelectedView => _selectedView;

        public event Action<V, bool> ViewSelected; //true when select, false when deselect


        public void SelectView(V view, bool clearSame)
        {
            if (view == null)
                return;

            if (_selectedView != null)
            {
                ViewSelected?.Invoke(_selectedView, false);
            }

            if (_selectedView == view) //clear if same
            {
                _selectedView = null;
            }
            else
            {
                _selectedView = view;
                ViewSelected?.Invoke(_selectedView, true);
            }
        }
    }
}
