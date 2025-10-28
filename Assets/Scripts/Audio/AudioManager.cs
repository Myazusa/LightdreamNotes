using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    /// <summary>
    /// 音频管理类，不提供直接操控方法，也不推荐直接获取该实例操控，建议通过事件进行操控以便触发其他监听事件
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [SerializeField] public AudioMixer mainMixer;

        void Awake()
        {
            if (Instance && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            // 加载AudioMixer结构树资源
            if (!mainMixer)
            {
                mainMixer = Resources.Load<AudioMixer>("Audio/MainMixer");
            }

            DontDestroyOnLoad(gameObject);
        }
    }
}