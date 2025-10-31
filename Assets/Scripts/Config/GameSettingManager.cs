using System;
using Enum;
using Event.SystemEvent;
using UnityEngine;

namespace Config
{
    public static class GameSettingManager
    {
        // 配置里的配置名称
        public const string BgmVolume = "BgmVolume";
        public const string MasterVolume = "MasterVolume";
        public const string SfxVolume = "SfxVolume";
        public const string VoiceVolume = "VoiceVolume";

        public const string ScreenWidth = "ScreenWidth";
        public const string ScreenHeight = "ScreenHeight";
        public const string Fullscreen = "Fullscreen";

        private static void InitializeDefaultSettings()
        {
            if (!PlayerPrefs.HasKey(MasterVolume)) PlayerPrefs.SetFloat(MasterVolume, DefaultPlayerPrefsSettings.MasterVolume);
            if (!PlayerPrefs.HasKey(BgmVolume)) PlayerPrefs.SetFloat(BgmVolume, DefaultPlayerPrefsSettings.BgmVolume);
            if (!PlayerPrefs.HasKey(SfxVolume)) PlayerPrefs.SetFloat(SfxVolume, DefaultPlayerPrefsSettings.SfxVolume);
            if (!PlayerPrefs.HasKey(VoiceVolume)) PlayerPrefs.SetFloat(VoiceVolume, DefaultPlayerPrefsSettings.VoiceVolume);

            if (!PlayerPrefs.HasKey(ScreenWidth)) PlayerPrefs.SetFloat(ScreenWidth, DefaultPlayerPrefsSettings.ScreenWidth);
            if (!PlayerPrefs.HasKey(ScreenHeight)) PlayerPrefs.SetFloat(ScreenHeight, DefaultPlayerPrefsSettings.ScreenHeight);
            if (!PlayerPrefs.HasKey(Fullscreen)) PlayerPrefs.SetInt(Fullscreen, Convert.ToInt32(DefaultPlayerPrefsSettings.Fullscreen));

            PlayerPrefs.Save();
        }

        /// <summary>
        /// 重置配置文件中音频音量的值为默认值
        /// </summary>
        public static void ResetAudioSettings()
        {
            if (PlayerPrefs.HasKey(MasterVolume)) PlayerPrefs.SetFloat(MasterVolume, DefaultPlayerPrefsSettings.MasterVolume);
            if (PlayerPrefs.HasKey(BgmVolume)) PlayerPrefs.SetFloat(BgmVolume, DefaultPlayerPrefsSettings.BgmVolume);
            if (PlayerPrefs.HasKey(SfxVolume)) PlayerPrefs.SetFloat(SfxVolume, DefaultPlayerPrefsSettings.SfxVolume);
            if (PlayerPrefs.HasKey(VoiceVolume)) PlayerPrefs.SetFloat(VoiceVolume, DefaultPlayerPrefsSettings.VoiceVolume);

            PlayerPrefs.Save();
        }

        /// <summary>
        /// 设置并保存配置，如果传入不存在的就会新建配置
        /// </summary>
        /// <param name="key">配置名</param>
        /// <param name="value">配置值</param>
        /// <typeparam name="T">只能是int，float，string三种类型，其他不能存也不处理</typeparam>
        public static void SaveSettings<T>(string key, T value)
        {
            if (!PlayerPrefs.HasKey(key)) return;
            switch (value)
            {
                case int intValue:
                    PlayerPrefs.SetInt(key, intValue);
                    PlayerPrefs.Save();
                    break;
                case float floatValue:
                    PlayerPrefs.SetFloat(key, floatValue);
                    PlayerPrefs.Save();
                    break;
                case string stringValue:
                    PlayerPrefs.SetString(key, stringValue);
                    PlayerPrefs.Save();
                    break;
            }


        }

        /// <summary>
        /// 读取音量配置
        /// </summary>
        /// <param name="commandType">要读取哪种音量</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static float LoadAudioSettings(AudioVolumeEvent.CommandType commandType)
        {
            InitializeDefaultSettings();
            switch (commandType)
            {
                case AudioVolumeEvent.CommandType.Master:
                    return PlayerPrefs.GetFloat(MasterVolume, DefaultPlayerPrefsSettings.MasterVolume);
                case AudioVolumeEvent.CommandType.BgmVolume:
                    return PlayerPrefs.GetFloat(BgmVolume, DefaultPlayerPrefsSettings.BgmVolume);
                case AudioVolumeEvent.CommandType.SfxVolume:
                    return PlayerPrefs.GetFloat(SfxVolume, DefaultPlayerPrefsSettings.SfxVolume);
                case AudioVolumeEvent.CommandType.VoiceVolume:
                    return PlayerPrefs.GetFloat(VoiceVolume, DefaultPlayerPrefsSettings.VoiceVolume);
                default:
                    throw new ArgumentException("传入参数错误");
            }
        }

        /// <summary>
        /// 读取屏幕尺寸配置
        /// </summary>
        /// <param name="screenAxis">要读取长还是宽</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static int LoadScreenSizeSettings(ScreenAxis screenAxis)
        {
            InitializeDefaultSettings();
            switch (screenAxis)
            {
                case ScreenAxis.ScreenWidth:
                    return PlayerPrefs.GetInt(ScreenWidth, Screen.currentResolution.width);
                case ScreenAxis.ScreenHeight:
                    return PlayerPrefs.GetInt(ScreenHeight, Screen.currentResolution.height);
                default:
                    throw new ArgumentException("传入参数错误");
            }
        }

        /// <summary>
        /// 读取是否开启了全屏
        /// </summary>
        /// <returns></returns>
        public static bool LoadFullscreenSettings()
        {
            InitializeDefaultSettings();
            return PlayerPrefs.GetInt(Fullscreen, 1) == 1;
        }
    }
}