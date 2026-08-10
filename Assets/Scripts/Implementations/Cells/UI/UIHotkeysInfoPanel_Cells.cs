using EasyField.Inputs;
using TMPro;
using UnityEngine;
using Zenject;

namespace EasyField.Implementations.Cells.UI
{
    public class UIHotkeysInfoPanel_Cells : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _lmbPaintText;
        [SerializeField]
        private TMP_Text _rmbPaintText;
        [SerializeField]
        private TMP_Text _markStartText;
        [SerializeField]
        private TMP_Text _markFinishText;

        
        [Inject]
        public void Construct(InputSettings inputSettings)
        {
            SetMarkStartText(inputSettings.MarkingKey.ToString());
            SetMarkFinishText(inputSettings.MarkingKey.ToString());
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
    }
}