using EasyField.Inputs;
using TMPro;
using UnityEngine;
using Zenject;

namespace EasyField.Implementations.Vertexes.UI
{
    public class UIHotkeysInfoPanel_Vertexes : MonoBehaviour
    {
        [Header("Left panel")]
        [SerializeField]
        private TMP_Text _lmbNodeText;
        [SerializeField]
        private TMP_Text _rmbNodeText;

        [Header("Right panel")]
        [SerializeField]
        private TMP_Text _markStartText;
        [SerializeField]
        private TMP_Text _markFinishText;
        [SerializeField]
        private TMP_Text _lmbLinkText;
        [SerializeField]
        private TMP_Text _rmbLinkText;


        [Inject]
        public void Construct(InputSettings inputSettings)
        {
            SetLMBNodeText(inputSettings.CreatingKey.ToString());
            SetRMBNodeText(inputSettings.CreatingKey.ToString());
            SetMarkStartText(inputSettings.MarkingKey.ToString());
            SetMarkFinishText(inputSettings.MarkingKey.ToString());
            SetLMBLinkText(inputSettings.LinkingKey.ToString());
            SetRMBLinkText(inputSettings.LinkingKey.ToString());
        }

        public void SetLMBNodeText(string btnArg)
        {
            var txt = "{0} + LMB - create new node (click on empty area)";
            _lmbNodeText.text = string.Format(txt, btnArg);
        }

        public void SetRMBNodeText(string btnArg)
        {
            var txt = "{0} + RMB - delete node";
            _rmbNodeText.text = string.Format(txt, btnArg);
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

        public void SetLMBLinkText(string btnArg)
        {
            var txt = "{0} + LMB - create link (needs selected node)";
            _lmbLinkText.text = string.Format(txt, btnArg);
        }

        public void SetRMBLinkText(string btnArg)
        {
            var txt = "{0} + RMB - delete link (needs selected node)";
            _rmbLinkText.text = string.Format(txt, btnArg);
        }
    }
}