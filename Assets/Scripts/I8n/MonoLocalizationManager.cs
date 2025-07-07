using System.Collections.Generic;
using System.IO;
using Enum;
using UnityEngine;
using Newtonsoft.Json;

namespace I8n
{
    // public class MonoLocalizationManager:MonoBehaviour
    // {
    //     public static MonoLocalizationManager Instance;
    //
    //     private Dictionary<string, string> _localizedText;
    //     private LangCode _currentLanguage = LangCode.zh;
    //
    //     private void Awake() {
    //         if (ReferenceEquals(Instance,null)) {
    //             Instance = this;
    //             DontDestroyOnLoad(gameObject);
    //             LoadLanguage(_currentLanguage);
    //         }
    //     }
    //
    //     public void LoadLanguage(LangCode langCode) {
    //         _currentLanguage = langCode;
    //         var path = Path.Combine(Application.streamingAssetsPath, $"i8n/{langCode.ToString()}.json");
    //
    //         if (File.Exists(path)) {
    //             var json = File.ReadAllText(path);
    //             _localizedText = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
    //         } else {
    //             Debug.LogError("Localization file not found: " + path);
    //             _localizedText = new Dictionary<string, string>();
    //         }
    //     }
    //
    //     public string Get(string key) {
    //         // 如果没有会把丢失的key显示在UI上
    //         return _localizedText.TryGetValue(key, out var value) ? value : $"!{key}!";
    //     }
    // }
}