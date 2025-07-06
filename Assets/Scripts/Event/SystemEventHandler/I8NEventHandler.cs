using Enum;
using Event.SystemEvent;
using I8n;
using UnityEngine;

namespace Event.SystemEventHandler
{
    public static class I8NEventHandler
    {
        /// <summary>
        /// 刷新所有文本，刷新后会变为对应加载的本地化语言
        /// </summary>
        /// <param name="e"></param>
        public static void HandleRefreshAllLocalizedText(ref I8NEvent e) {
            if (e.Command != I8NEvent.CommandType.RefreshAllLocalizedText) return;

            foreach (var t in Object.FindObjectsByType<MonoLocalizedText>(FindObjectsSortMode.None)) {
                t.UpdateText();
            }
            MonoFontManager.Instance.ApplyFontToAllTMP();
        }

        /// <summary>
        /// 切换语言并切换字体，但是并不重载文本为对应语言
        /// </summary>
        /// <param name="e"></param>
        public static void HandleSwitchLanguage(ref I8NEvent e)
        {
            if (e.Command != I8NEvent.CommandType.SwitchLanguage) return;

            // 读取i8n的json文件，存在对象里
            MonoLocalizationManager.Instance.LoadLanguage(e.LangCode);

            // 设置字体的当前语言，并且应用到所有的TMP上
            MonoFontManager.Instance.currentLanguage = e.LangCode.ToString();
            MonoFontManager.Instance.ApplyFontToAllTMP();
        }
    }
}
