using Core.Nodes;

namespace Core.Links
{
    public struct Link<T> : ILink<T> where T : class, INode<T>
    {
        private T _from;
        private T _to;
        private float _cost;

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