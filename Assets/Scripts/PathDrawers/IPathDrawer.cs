using ThisProject.Views;
using System.Collections.Generic;

namespace ThisProject.PathDrawers
{
    public interface IPathDrawer<V> where V : IView
    {
        public void SetPath(IReadOnlyList<V> path);
        public void ShowPath(bool show);
    }
}