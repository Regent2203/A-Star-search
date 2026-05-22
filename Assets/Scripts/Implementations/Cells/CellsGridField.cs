using Core.Fields.Grids;
using System;
using UnityEngine;
using Zenject;

namespace Core.Implementations.Cells
{
    public class CellsGridField : GridField<CellNode, CellView>
    {
        private CellsGridFieldGenerator _generator;

        public event Action FieldChanged; //todo


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

        private void OnNodePositionChanged(Vector2 pos)
        {
            //todo
        }

        private void OnNodeTypeChanged(CellNode node, CellType cellType)
        {
            var view = GetViewById(node.Id);
            view.UpdateSprite(cellType.Sprite);

            FieldChanged?.Invoke();
        }
    }
}