using Core.Fields;
using System;
using Zenject;

namespace Core.Implementations.Vertexes
{
    public class VertexesField : IGraph<VertexNode, int>
    {
        //private CellsGridGenerator _generator;

        //public event Action<CellNode, CellType> CellNodeTypeChanged;

        /*
        [Inject]
        public void Construct(CellsGridGenerator generator)
        {
            _generator = generator;
        }*/
        public VertexNode GetNodeById(int id)
        {
            throw new NotImplementedException();
        }
    }
}