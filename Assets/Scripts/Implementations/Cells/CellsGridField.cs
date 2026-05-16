using Core.Fields.Grids;
using System;
using Zenject;

namespace Core.Implementations.Cells
{
    public class CellsGridField : GridField<CellNode, CellView>
    {
        private CellsGridFieldGenerator _generator;

        public event Action GridTopologyChanged;


        [Inject]
        public void Construct(CellsGridFieldGenerator generator)
        {
            _generator = generator;
        }

        protected override void Init()
        {
            base.Init();

            //todo: change if we want to call this method not at scene start (instead: after we change grid size or else)
            _generator.PopulateField(this, transform, _scaleFactor, _grid, OnNodeTypeChanged);
        }

        private void OnNodeTypeChanged(CellNode node, CellType cellType)
        {
            var view = GetViewForNode(node);
            view.UpdateSprite(cellType.Sprite);

            GridTopologyChanged?.Invoke();
        }
    }
}