using Core.Fields.Grids;
using System;
using Zenject;

namespace Core.Implementations.Cells
{
    public class CellsGridField : GridField<CellNode, CellView>
    {
        private CellsGridGenerator _generator;

        public event Action<CellNode, CellType> CellNodeTypeChanged;


        [Inject]
        public void Construct(CellsGridGenerator generator)
        {
            _generator = generator;
        }

        protected override void Init()
        {
            base.Init();

            //todo: change if we want to call this method not at scene start (instead: after we change grid size or else)
            _generator.PopulateField(this, transform, _scaleFactor, _grid, NotifyNodeTypeChanged);
        }

        public void NotifyNodeTypeChanged(CellNode node, CellType cellType)
        {
            CellNodeTypeChanged?.Invoke(node, cellType);
        }
    }
}