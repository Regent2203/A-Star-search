using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace EasyField.UICommon
{
    /// <summary>
    /// Updates the value of Scrollbar at the start of the game, because layout groups ruin it in builds
    /// </summary>
    [RequireComponent(typeof(Scrollbar))]
    public class ScrollbarFixer : MonoBehaviour
    {
        [SerializeField]
        private Scrollbar _scrollbar;
        [SerializeField]
        private float _value = 1.0f;


        private void Start()
        {
            StartCoroutine(ValueFix(_value));
        }

        private IEnumerator ValueFix(float value)
        {
            yield return new WaitForEndOfFrame();
            
            _scrollbar.value = value;
        }

        private void Reset()
        {
            _scrollbar = GetComponent<Scrollbar>();
        }
    }
}