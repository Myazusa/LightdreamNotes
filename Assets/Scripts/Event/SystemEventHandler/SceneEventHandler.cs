using System.Collections;
using Event.SystemEvent;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Event.SystemEventHandler
{
    public static class SceneEventHandler
    {
        /// <summary>
        /// 切换主游戏场景的方法
        /// </summary>
        /// <param name="e"></param>
        public static void HandleToMainGameScene(ref SceneEvent e)
        {
            if (e.Command != SceneEvent.CommandType.ToMainGameScene) return;

            e.Context.StartCoroutine(LoadSceneAsync("MainGameScene"));
            IEnumerator LoadSceneAsync(string sceneName)
            {
                var asyncLoad = SceneManager.LoadSceneAsync(sceneName);
                if (asyncLoad != null)
                {
                    while (!asyncLoad.isDone)
                    {
                        yield return null;
                    }
                }
            }
        }
        /// <summary>
        /// 退出游戏的方法
        /// </summary>
        /// <param name="e"></param>
        public static void HandleGameQuit(ref SceneEvent e)
        {
            if (e.Command != SceneEvent.CommandType.GameQuit) return;
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            // 游戏直接退出，全平台通用
            Application.Quit();
#endif
        }
    }
}