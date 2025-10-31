using Audio;
using Event.SystemEvent;
using UnityEngine;

namespace Event.SystemEventHandler
{
    public static class GameSettingEventHandler
    {
        /// <summary>
        /// 变更音量的方法
        /// </summary>
        /// <param name="e"></param>
        public static void HandleChangeVolume(ref AudioVolumeEvent e)
        {
            // 如果本地配置没有则用默认值

            var dB = Mathf.Log10(Mathf.Clamp(e.Volume, 0.0001f, 1f)) * 20f;
            AudioManager.Instance.mainMixer.SetFloat(e.Command.ToString(), dB);
        }

        /// <summary>
        /// 变更窗口尺寸的方法
        /// </summary>
        /// <param name="e"></param>
        public static void HandleSetWindowSize(ref ScreenSizeEvent e)
        {
            // 只变更尺寸，不变更是否全屏
            switch (e.Command)
            {
                case ScreenSizeEvent.CommandType.Size720:
                    Screen.SetResolution(1280, 720, Screen.fullScreen);
                    break;
                case ScreenSizeEvent.CommandType.Size900:
                    Screen.SetResolution(1600, 900, Screen.fullScreen);
                    break;
                case ScreenSizeEvent.CommandType.Size1080:
                    Screen.SetResolution(1920, 1080, Screen.fullScreen);
                    break;
            }
        }

        /// <summary>
        /// 切换全屏的方法
        /// </summary>
        /// <param name="e"></param>
        public static void HandleSwitchFullScreen(ref ScreenSizeEvent e)
        {
            if (e.Command != ScreenSizeEvent.CommandType.SwitchFullScreen) return;
            Screen.fullScreen = !Screen.fullScreen;
        }
    }
}