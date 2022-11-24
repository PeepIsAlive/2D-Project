using Leopotam.Ecs;
using Events;
using Tags;
using Components;
using UnityEngine;

namespace Systems
{
    public sealed class PlayerJumpSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTag, JumpComponent, JumpEvent> _jumpFilter;

        public void Run()
        {
            if (_jumpFilter.IsEmpty())
                return;

            foreach (var i in _jumpFilter)
            {
                ref var playerEntity = ref _jumpFilter.GetEntity(i);

                if (playerEntity.Has<MovableComponent>())
                {
                    ref var rigidbody = ref playerEntity.Get<MovableComponent>().Rigidbody;
                    ref var jumpForce = ref _jumpFilter.Get2(i).JumpForce;

                    Jump(rigidbody, jumpForce);
                }
            }
        }

        private void Jump(Rigidbody2D rigidbody, float jumpForce)
        {
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
