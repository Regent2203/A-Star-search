using ThisProject.Fields.Implementations;
using System;
using UnityEngine;
using Zenject;
using ThisProject.Fields.NodeMovers;

namespace ThisProject.Implementations.Cells
{
    public class CellsGridField : GridSceneField<CellNode, CellView>
    {
        private CellsGridFieldGenerator _generator;
        private NullNodeMover _nodeMover;

        public override INodeMover NodeMover => _nodeMover;


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