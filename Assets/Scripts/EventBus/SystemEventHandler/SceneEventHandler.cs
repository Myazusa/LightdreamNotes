using System.Collections;
using EventBus.SystemEvent;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EventBus.SystemEventHandler
{
    public static class SceneEventHandler
    {
        /// <summary>
        /// 切换为主游戏场景
        /// </summary>
        /// <param name="e"></param>
        public static void HandleToMainGameScene(ref SceneEvent e)
        {
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
        /// 退出游戏
        /// </summary>
        /// <param name="e"></param>
        public static void HandleGameQuit(ref SceneEvent e)
        {
            // 游戏直接退出，全平台通用
            Application.Quit();
        }
    }
}