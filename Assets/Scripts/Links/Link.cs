using ThisProject.Nodes;

namespace ThisProject.Links
{
    public struct Link<T> : ILink<T> where T : class, INode
    {
        private readonly T _from;
        private readonly T _to;
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

        public void ChangeCost(float value)
        {
            _cost += value; 
        }
    }
}