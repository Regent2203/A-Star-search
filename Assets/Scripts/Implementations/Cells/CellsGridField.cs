using ThisProject.Fields.Grids;
using System;
using UnityEngine;
using Zenject;

namespace ThisProject.Implementations.Cells
{
    public class CellsGridField : VisibleGridField<CellNode, CellView>
    {
        private CellsGridFieldGenerator _generator;


        [Inject]
        public void Construct(CellsGridFieldGenerator generator)
        {
            _generator = generator;
        }

        protected override void Init()
        {
            base.Init();

            //todo: change if we want to call this method not at scene start (instead: after we change grid size or else)
            _generator.SetConfiguration(this, transform, _scaleFactor, OnNodePositionChanged, OnNodeTypeChanged);
            _generator.PopulateField();
        }

        

        private void OnNodeTypeChanged(CellNode node, CellType cellType)
        {
            var view = GetViewById(node.Id);
            view.UpdateSprite(cellType.Sprite);

            NotifyFieldChanged();
        }
    }
}