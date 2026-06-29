using ThisProject.Nodes;

namespace ThisProject.Links
{
    public class LinkData<T> : ILinkData<T>
        where T : INodeData
    {
        private readonly T _from;
        private readonly T _to;
        private float _cost;

        public T From => _from;
        public T To => _to;
        public float Cost => _cost;


        public LinkData(T from, T to, float cost)
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