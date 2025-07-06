using UnityEngine;

namespace Entity.LivingEntity.Player
{
    public class MonoPlayer: MonoBehaviour
    {
        // 持有本框架定义类
        private PlayerLivingEntity _playerLivingEntity;

        private void Start()
        {
            // 反向注入
            _playerLivingEntity = new PlayerLivingEntity(this,200,1.0f,2.0f);
            _playerLivingEntity.InitPlayerControl();
        }

        private void Update()
        {
            _playerLivingEntity.UpdatePlayerMove();
        }

    }
}