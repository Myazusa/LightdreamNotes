using Enum;
using I8n;
using UnityEngine;

namespace EventBus.Event
{
    public class I8NEvent :MonoBehaviour
    {
        public void RefreshAllLocalizedTextEvent() {
            foreach (var t in FindObjectsByType<MonoLocalizedText>(FindObjectsSortMode.None)) {
                t.UpdateText();
            }
            MonoFontManager.Instance.ApplyFontToAllTMP();
        }

        public void SwitchLanguageEvent(LangCode langCode)
        {
            // 读取i8n的json文件，存在对象里
            MonoLocalizationManager.Instance.LoadLanguage(langCode);

            // 设置字体的当前语言，并且应用到所有的TMP上
            MonoFontManager.Instance.currentLanguage = langCode.ToString();
            MonoFontManager.Instance.ApplyFontToAllTMP();
        }
    }
}
