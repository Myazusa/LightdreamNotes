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
            var dB = Mathf.Log10(Mathf.Clamp(e.Volume, 0.0001f, 1f)) * 20f;
            AudioManager.Instance.mainMixer.SetFloat(e.Command.ToString(), dB);
        }

        /// <summary>
        /// 变更窗口尺寸的方法
        /// </summary>
        /// <param name="e"></param>
        public static void HandleSetWindowSize(ref WindowSizeEvent e)
        {
            switch (e.Command)
            {
                case WindowSizeEvent.CommandType.Size720:
                    Screen.SetResolution(1280, 720, false);
                    break;
                case WindowSizeEvent.CommandType.Size1080:
                    Screen.SetResolution(1920, 1080, false);
                    break;
            }
        }

        /// <summary>
        /// 切换全屏的方法
        /// </summary>
        /// <param name="e"></param>
        public static void HandleSwitchFullScreen(ref WindowSizeEvent e)
        {
            if (e.Command != WindowSizeEvent.CommandType.SwitchFullScreen) return;
            Screen.fullScreen = !Screen.fullScreen;
        }
    }
}