using Core.Inputs;
using TMPro;
using UnityEngine;
using Zenject;

namespace Core.Implementations.Cells.UI
{
    public class UICellsPaletteHotkeyInfoPanel : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _lmbText;
        [SerializeField]
        private TMP_Text _rmbText;
        [SerializeField]
        private TMP_Text _markStartText;
        [SerializeField]
        private TMP_Text _markFinishText;

        private const string CellTypeFormat = "{0} - {1}";
        private const string CellMarkFormat = "{0} + {1} - {2}";

        [Inject]
        public void Construct(InputSettings inputSettings)
        {
            SetMarkStartText(inputSettings.MarkingKey.ToString());
            SetMarkFinishText(inputSettings.MarkingKey.ToString());
        }

        public void SetLMBText(string cellTypeArg)
            => SetText(_lmbText, string.Format(CellTypeFormat, "LMB", cellTypeArg));

        public void SetRMBText(string cellTypeArg)
            => SetText(_rmbText, string.Format(CellTypeFormat, "RMB", cellTypeArg));

        public void SetMarkStartText(string btnArg)
            => SetText(_markStartText, string.Format(CellMarkFormat, btnArg, "LMB", "start"));

        public void SetMarkFinishText(string btnArg)
            => SetText(_markFinishText, string.Format(CellMarkFormat, btnArg, "RMB", "finish"));

        private void SetText(TMP_Text label, string text)
        {
            label.text = text;
        }
    }
}