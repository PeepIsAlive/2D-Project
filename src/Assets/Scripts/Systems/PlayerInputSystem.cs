using Leopotam.Ecs;
using Components;
using Tags;
using UnityEngine;
using Events;

namespace Systems
{
    public sealed class PlayerInputSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTag, JumpComponent> _jumpFilter;
        private readonly EcsFilter<PlayerTag, DirectionComponent> _directionFilter;

        private PlayerInputActions _inputActions;
        private Vector2 _direction;

        public void Init() => InputInitialize();

        public void Run()
        {
            SetDirection();

            foreach (var i in _directionFilter)
            {
                ref var direction = ref _directionFilter.Get2(i).Direction;

                direction.x = _direction.x;
            }
        }

        private void SetDirection()
        {
            _direction.x = _inputActions.Movement.Move.ReadValue<Vector2>().x;
        }

        private void InputInitialize()
        {
            _inputActions = new PlayerInputActions();
            _inputActions.Movement.Jump.performed += _ =>
            {
                foreach (var i in _jumpFilter)
                {
                    _jumpFilter.GetEntity(i).Replace(new JumpEvent());
                }
            };

            _inputActions.Movement.Enable();
        }
    }
}
