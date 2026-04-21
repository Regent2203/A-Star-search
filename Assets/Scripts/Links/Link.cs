using Core.Nodes;

namespace Core.Links
{
    public class Link<T> : ILink<T> where T : INode<T>
    {
        protected T _from;
        protected T _to;
        protected float _cost;

        public T From => _from;
        public T To => _to;
        public float Cost => _cost;


        public Link(T from, T to, float cost)
        {
            _from = from;
            _to = to;
            _cost = cost;
        }
    }
}
