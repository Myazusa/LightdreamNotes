using UnityEngine;
using UnityEngine.InputSystem;

namespace Entity.LivingEntity.Player
{
    public class PlayerLivingEntity :ILivingEntity
    {
        private readonly MonoPlayer _player;
        // 这个类在Inputs里被生成
        private PlayerInput _playerInput;
        private Vector2 _moveVector;
        public uint Health { get; set; }
        public float WalkSpeed{get;set;}
        public float RunSpeed {get;set;}
        public PlayerLivingEntity(MonoPlayer monoPlayer, uint health = 100, float walkSpeed = 1f, float runSpeed = 1.5f)
        {
            _player = monoPlayer;
            Health = health;
            WalkSpeed = walkSpeed;
            RunSpeed = runSpeed;
        }

        public void MoveEntity(Vector3 movement)
        {
            movement *= WalkSpeed;
            _player.transform.position += movement;
            // todo: 这里实现自动操控
        }

        public void DamageEntity(uint damage)
        {
            Health -= damage;
        }

        public void MoveEntity(InputAction.CallbackContext context)
        {
            _moveVector = context.ReadValue<Vector2>();
        }

        public void InitPlayerControl()
        {
            _playerInput = new PlayerInput();
            _playerInput.PlayerControl.Enable();

            // 这里是要订阅的控制事件
            _playerInput.PlayerControl.Move.performed += MoveEntity;
        }

        public void UpdatePlayerMove()
        {
            if (_moveVector != Vector2.zero)
            {
                _player.transform.position += new Vector3(_moveVector.x, _moveVector.y);
            }
        }
    }
}

