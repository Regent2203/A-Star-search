using Core.Heuristic;
using Core.Heuristic.Functions;

namespace Core.Implementations.Vertexes
{
    public class VertexesHeuristicsProvider : HeuristicsProvider<VertexNode>
    {
        public VertexesHeuristicsProvider(IHeuristicFunction heuristicFunction)
            : base(heuristicFunction, 1.0f) { }
    }
}