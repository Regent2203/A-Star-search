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

        protected void Awake()
        {
            ConfigureGenerator();
        }

        private void ConfigureGenerator()
        {
            _generator.SetConfiguration(this, transform);
        }
    }
}