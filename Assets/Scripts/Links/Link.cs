using Core.Nodes;

namespace Core.Links
{
    public struct Link<T, TId> : ILink<T, TId> where T : class, INode<T, TId>
    {
        private T _from;
        private T _to;
        private float _cost;

        public readonly T From => _from;
        public readonly T To => _to;
        public readonly float Cost => _cost;


        public Link(T from, T to, float cost)
        {
            _from = from;
            _to = to;
            _cost = cost;
        }
    }
}