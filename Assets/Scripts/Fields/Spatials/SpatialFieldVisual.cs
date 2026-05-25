using ThisProject.Views;
using ThisProject.ObjectsStorages;
using System.Collections.Generic;

namespace ThisProject.Fields.Grids
{
    public class SpatialFieldVisual<V> : IFieldVisual<V, int> where V : class, IView<int>
    {
        protected DictTypeStorage<V> _views;
        public IObjectsStorage<V, int> Views => _views;


        public SpatialFieldVisual(DictTypeStorage<V> views)
        {
            _views = views;
        }

        public void SetViewsData(Dictionary<int, V> data) => _views.SetData(data);
    }
}