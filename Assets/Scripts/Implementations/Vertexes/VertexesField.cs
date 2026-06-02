using ThisProject.Fields;
using Zenject;

namespace ThisProject.Implementations.Vertexes
{
    public class VertexesField : SpatialField<VertexNode, VertexView, int>
    {
        private VertexesFieldGenerator _generator;


        [Inject]
        public void Construct(VertexesFieldGenerator generator)
        {
            _generator = generator;
        }

        protected override void Init()
        {
            base.Init();
            
            //todo
            _generator.SetConfiguration(this, transform);
            _generator.TestPopulate();
        }

        public void AddFieldData(VertexNode node, VertexView view)
        {
            //_nodes.Add(node.Id, node);
            //_views.Add(view.Id, view);
        }
    }
}