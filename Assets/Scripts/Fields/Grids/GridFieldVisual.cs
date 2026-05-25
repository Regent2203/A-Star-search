using ThisProject.Views;
using ThisProject.ObjectsStorages;
using UnityEngine;

namespace ThisProject.Fields.Grids
{
    public class GridFieldVisual<V> : IFieldVisual<V, Vector2Int> where V : class, IView<Vector2Int>
    {
        protected GridTypeStorage<V> _views;
        public IObjectsStorage<V, Vector2Int> Views => _views;


        public GridFieldVisual(GridTypeStorage<V> views)
        {
            _views = views;
        }

        public void SetViewsData(V[,] data) => _views.SetData(data);
    }
}