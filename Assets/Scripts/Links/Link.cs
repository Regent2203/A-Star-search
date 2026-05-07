using Core.Nodes;

namespace Core.Links
{
    public struct Link : ILink
    {
        private INode _from;
        private INode _to;
        private float _cost;

        public INode From => _from;
        public INode To => _to;
        public float Cost => _cost;


        public Link(INode from, INode to, float cost)
        {
            _from = from;
            _to = to;
            _cost = cost;
        }
    }
}