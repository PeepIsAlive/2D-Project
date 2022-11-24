using Leopotam.Ecs;
using Components;
using UnityEngine;

namespace Systems
{
    public sealed class PlayerMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MovableComponent, DirectionComponent> _movableFilter;

        public void Run()
        {
            foreach (var i in _movableFilter)
            {
                ref var rigidbody = ref _movableFilter.Get1(i).Rigidbody;
                ref var moveSpeed = ref _movableFilter.Get1(i).MoveSpeed;
                ref var direction = ref _movableFilter.Get2(i).Direction;

                Move(rigidbody, direction, moveSpeed);
            }
        }

        private void Move(Rigidbody2D rigidbody, Vector2 direction, float speed)
        {
            if (direction.x == 0f)
            {
                rigidbody.velocity = new Vector2(0f, rigidbody.velocity.y);
                return;
            }

            if (rigidbody.velocity.x > speed / 2)
            {
                rigidbody.velocity = new Vector2(speed / 2, rigidbody.velocity.y);
            }
            else if (rigidbody.velocity.x < -speed / 2)
            {
                rigidbody.velocity = new Vector2(-speed / 2, rigidbody.velocity.y);
            }

            rigidbody.velocity += direction * speed * Time.fixedUnscaledDeltaTime;
        }
    }
}
