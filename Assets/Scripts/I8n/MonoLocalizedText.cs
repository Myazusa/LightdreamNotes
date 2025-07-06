using TMPro;
using UnityEngine;

namespace I8n
{
    public class MonoLocalizedText:MonoBehaviour
    {
        public string key;
        private TMP_Text _text;

        private void Awake() {
            _text = GetComponent<TMP_Text>();
        }
        private void Start() {
            UpdateText();
        }

        public void UpdateText() {
            // todo:修复这里异常
            if (ReferenceEquals(_text,null)) _text = GetComponent<TMP_Text>();
            _text.text = MonoLocalizationManager.Instance.Get(key);
        }
    }
}