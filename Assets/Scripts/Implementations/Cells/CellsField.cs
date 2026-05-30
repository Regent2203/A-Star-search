using ThisProject.Fields;
using Zenject;

namespace ThisProject.Implementations.Cells
{
    public class CellsField : GridField<CellNode, CellView>
    {
        private CellsFieldGenerator _generator;


        [Inject]
        public void Construct(CellsFieldGenerator generator)
        {
            _generator = generator;
        }

        protected override void Init()
        {
            base.Init();

            ConfigureGenerator();
        }

        private void ConfigureGenerator()
        {
            _generator.SetConfiguration(this, transform, _scaleFactor);
        }
    }
}