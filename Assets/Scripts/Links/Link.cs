using Algorithm;

namespace Links
{
    public class Link : ILink
    {
        private INode _from;
        protected INode _to;
        protected float _cost;

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
