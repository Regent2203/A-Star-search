using ThisProject.Fields;
using Zenject;

namespace ThisProject.Implementations.Cells
{
    public class CellsGridField : GridField<CellNode, CellView>
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



            //todo: change if we want to call this method not at scene start (instead: after we change grid size or else)
            _generator.SetConfiguration(this, transform, _scaleFactor);
            _generator.PopulateField();

            //NodeTypeChanged += UpdateSpriteForCellType; //todo
        }

        private void UpdateSpriteForCellType(CellNode node, CellType cellType)
        {
            var view = GetViewById(node.Id);
            view.UpdateSprite(cellType.Sprite);
        }
    }
}