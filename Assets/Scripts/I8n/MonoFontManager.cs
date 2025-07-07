using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace I8n
{
    // public class MonoFontManager:MonoBehaviour
    // {
    //     public static MonoFontManager Instance;
    //
    //     public List<FontPerLanguage> fontMap;
    //     private Dictionary<string, TMP_FontAsset> _fontDict;
    //
    //     public string currentLanguage = "en";
    //
    //     private void Awake() {
    //         if (!Instance) Instance = this;
    //         _fontDict = fontMap.ToDictionary(f => f.languageCode, f => f.font);
    //     }
    //
    //     public TMP_FontAsset GetFontForCurrentLanguage() {
    //         return _fontDict.GetValueOrDefault(currentLanguage);
    //     }
    //
    //     public void ApplyFontToAllTMP() {
    //         var tmpTexts = FindObjectsByType<TMP_Text>(FindObjectsSortMode.None);
    //         var font = GetFontForCurrentLanguage();
    //         foreach (var tmp in tmpTexts) {
    //             tmp.font = font;
    //         }
    //     }
    // }
}