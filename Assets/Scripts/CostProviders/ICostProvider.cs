using Core.Nodes;

namespace Core.CostProviders
{
    public interface ICostProvider
    {
        float GetCost(INode from, INode to);
    }
}