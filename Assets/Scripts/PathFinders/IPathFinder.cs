using Core.Nodes;
using System.Collections.Generic;

namespace Core.PathFinders
{
    public interface IPathFinder
    {
        public void UpdateStartNode(INode node);
        public void UpdateFinishNode(INode node);
        public IList<INode> GetPath();
    }
}