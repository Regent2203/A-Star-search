using System;
using ThisProject.Fields.Implementations;
using ThisProject.Fields.NodeMovers;
using Zenject;

namespace ThisProject.Implementations.Cells
{
    public class CellsGridField : GridSceneField<CellNode, CellView>
    {
        private CellsFieldGenerator _generator;
        private INodeMover _nodeMover;

        public override INodeMover NodeMover => _nodeMover;

        public event Action<CellNode, CellType> NodeTypeChanged;


        [Inject]
        public void Construct(CellsFieldGenerator generator, INodeMover nodeMover)
        {
            _generator = generator;
            _nodeMover = nodeMover;
        }

        protected override void Init()
        {
            base.Init();



            //todo: change if we want to call this method not at scene start (instead: after we change grid size or else)
            _generator.SetConfiguration(this, transform, _scaleFactor, NotifyNodeMoved, NotifyNodeTypeChanged);
            _generator.PopulateField();

            NodeTypeChanged += UpdateSpriteForCellType;
        }

        protected void NotifyNodeTypeChanged(CellNode node, CellType cellType)
        {
            NodeTypeChanged?.Invoke(node, cellType);
            NotifyFieldChanged();
        }

        private void UpdateSpriteForCellType(CellNode node, CellType cellType)
        {
            var view = GetViewById(node.Id);
            view.UpdateSprite(cellType.Sprite);
        }
    }
}