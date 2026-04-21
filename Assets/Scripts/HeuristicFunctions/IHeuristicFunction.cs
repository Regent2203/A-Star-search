namespace Core.HeuristicFunctions
{
    public interface IHeuristicFunction
    {
        public float EstimateCost(IView node1, IView node2);
    }
}
