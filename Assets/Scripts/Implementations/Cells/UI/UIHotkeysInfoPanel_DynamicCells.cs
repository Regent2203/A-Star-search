using EasyField.Inputs;
using TMPro;
using UnityEngine;
using Zenject;

namespace EasyField.Implementations.DynamicCells.UI
{
    public class UIHotkeysInfoPanel_DynamicCells : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _lmbPaintText;
        [SerializeField]
        private TMP_Text _rmbPaintText;
        [SerializeField]
        private TMP_Text _markStartText;
        [SerializeField]
        private TMP_Text _markFinishText;
        [SerializeField]
        private TMP_Text _lmbCreateText;
        [SerializeField]
        private TMP_Text _rmbCreateText;
        [SerializeField]
        private TMP_Text _linkingText;


        [Inject]
        public void Construct(InputSettings inputSettings)
        {
            SetMarkStartText(inputSettings.MarkingKey.ToString());
            SetMarkFinishText(inputSettings.MarkingKey.ToString());
            SetLMBCreateText(inputSettings.LinkingKey.ToString());
            SetRMBCreateText(inputSettings.LinkingKey.ToString());
            SetLinkingText(inputSettings.LinkingKey.ToString());
        }

        public void SetLMBPaintText(string cellTypeArg)
        {
            var txt = "LMB - paint cell ({0})";
            _lmbPaintText.text = string.Format(txt, cellTypeArg);
        }

        public void SetRMBPaintText(string cellTypeArg)
        {
            var txt = "RMB - paint cell ({0})";
            _rmbPaintText.text = string.Format(txt, cellTypeArg);
        }

        public void SetMarkStartText(string btnArg)
        {
            var txt = "{0} + LMB - set as Start";
            _markStartText.text = string.Format(txt, btnArg);
        }

        public void SetMarkFinishText(string btnArg)
        {
            var txt = "{0} + RMB - set as Finish";
            _markFinishText.text = string.Format(txt, btnArg);
        }

        public void SetLMBCreateText(string btnArg)
        {
            var txt = "{0} + LMB - create cell";
            _lmbCreateText.text = string.Format(txt, btnArg);
        }

        public void SetRMBCreateText(string btnArg)
        {
            var txt = "{0} + RMB - delete cell";
            _rmbCreateText.text = string.Format(txt, btnArg);
        }

        public void SetLinkingText(string btnArg)
        {
            var txt = "Use {0} + LMB/RMB to select cell. Then, for the selected cell, use {0} + LMB to create link or {0} + RMB to delete link.";
            _linkingText.text = string.Format(txt, btnArg);
        }
    }
}