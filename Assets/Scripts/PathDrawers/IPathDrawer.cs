using EasyField.Nodes;
using System.Collections.Generic;

namespace EasyField.PathDrawers
{
    public interface IPathDrawer<TNodeView>
        where TNodeView : INodeView
    {
        public void SetPath(IReadOnlyList<TNodeView> path);
        public void ShowPath(bool show);
    }
}