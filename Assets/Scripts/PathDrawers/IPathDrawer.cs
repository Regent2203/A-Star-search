using EasyField.Nodes;
using System.Collections.Generic;

namespace EasyField.PathDrawers
{
    public interface IPathDrawer<V>
        where V : INodeView
    {
        public void SetPath(IReadOnlyList<V> path);
        public void ShowPath(bool show);
    }
}