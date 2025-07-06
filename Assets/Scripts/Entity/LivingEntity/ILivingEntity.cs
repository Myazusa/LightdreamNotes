using UnityEngine;

namespace Entity.LivingEntity
{
    public interface ILivingEntity :IEntity
    {
        public uint Health{get;set;}
        public float WalkSpeed{get;set;}
        public float RunSpeed{get;set;}

        /// <summary>
        /// 实现这个实体要如何移动
        /// </summary>
        public abstract void MoveEntity(Vector3 movement);

        /// <summary>
        /// 实现这个实体要如何扣血
        /// </summary>
        public abstract void DamageEntity(uint damage);
    }
}