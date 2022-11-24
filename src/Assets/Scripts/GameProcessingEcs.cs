using Events;
using Leopotam.Ecs;
using Systems;
using UnityEngine;
using Voody.UniLeo;

public sealed class GameProcessingEcs : MonoBehaviour
{
    private EcsWorld _world;
    private EcsSystems _systems;
    private EcsSystems _fixedSystems;

    private void Awake()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);
        _fixedSystems = new EcsSystems(_world);

        AddSystems();

        AddFixedSystems();
        AddFixedOneFrames();

        _systems.ConvertScene();
        _fixedSystems.ConvertScene();

        _systems.Init();
        _fixedSystems.Init();
    }

    private void Update()
    {
        _systems.Run();
        _fixedSystems.Run();
    }

    private void OnDestroy()
    {
        if (_systems == null)
            return;

        _systems.Destroy();
        _systems = null;

        _fixedSystems.Destroy();
        _fixedSystems = null;

        _world.Destroy();
        _world = null;
    }

    private void AddSystems()
    {
        _systems
            .Add(new PlayerInputSystem())
            ;
    }

    private void AddFixedSystems()
    {
        _fixedSystems
            .Add(new PlayerMovementSystem())
            .Add(new PlayerJumpSystem())
            ;
    }

    private void AddFixedOneFrames()
    {
        _fixedSystems.OneFrame<JumpEvent>();
    }
}
