using ThisProject.Nodes;
using System.Collections.Generic;

namespace ThisProject.PathDrawers
{
    public interface IPathDrawer<V>
        where V : INodeView
    {
        public void SetPath(IReadOnlyList<V> path);
        public void ShowPath(bool show);
    }
}