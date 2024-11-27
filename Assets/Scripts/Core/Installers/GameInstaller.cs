using Core.Characters;
using Core.Characters.Interfaces;
using Zenject;
using UnityEngine;
using Core.Configs;
using Core.Services;

namespace Core.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameConfig gameConfig;

        public override void InstallBindings()
        {
            Container.Bind<GameConfig>().FromInstance(gameConfig).AsSingle();
            Container.Bind<GameStateService>().AsSingle();

            Container.BindInterfacesAndSelfTo<EnemyService>().AsSingle();
            Container.BindInterfacesAndSelfTo<GoldService>().AsSingle();
            Container.Bind<IPlayer>().FromMethod(CreatePlayer).AsSingle().NonLazy();;
        }

        private Player CreatePlayer()
        {
            return Container.InstantiatePrefabForComponent<Player>(gameConfig.playerPrefab);
        }
    }
}