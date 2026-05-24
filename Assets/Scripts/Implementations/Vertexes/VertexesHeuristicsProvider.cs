using ThisProject.Heuristic;
using ThisProject.Heuristic.Functions;

namespace ThisProject.Implementations.Vertexes
{
    public class VertexesHeuristicsProvider : HeuristicsProvider<VertexNode>
    {
        public VertexesHeuristicsProvider(IHeuristicFunction heuristicFunction)
            : base(heuristicFunction, 1.0f) { }
    }
}