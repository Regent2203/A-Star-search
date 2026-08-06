using EasyField.Heuristic;
using EasyField.Heuristic.Functions;

namespace EasyField.Implementations.Vertexes
{
    public class VertexesHeuristicsProvider : HeuristicsProvider<VertexData>
    {
        public VertexesHeuristicsProvider(IHeuristicFunction heuristicFunction)
            : base(heuristicFunction, 1.0f) { }
    }
}