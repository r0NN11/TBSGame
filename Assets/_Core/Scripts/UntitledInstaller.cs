using _Core.Scripts.Battle;
using _Core.Scripts.Effects;
using _Core.Scripts.Units.Enemy;
using _Core.Scripts.Units.Enemy.EnemyInject;
using _Core.Scripts.Units.Player;
using _Core.Scripts.Units.Player.PlayerInject;
using Zenject;

namespace _Core.Scripts
{
    public class UntitledInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<BattleManager>().FromInstance(FindObjectOfType<BattleManager>(true)).AsSingle();
            Container.Bind<GameStateDirector>().AsSingle();
            Container.Bind<PlayerController>().AsSingle();
            Container.Bind<EnemyController>().AsSingle();
            Container.Bind<SettingsSpawnCount>().FromScriptableObjectResource("SettingsSpawnCount").AsSingle();
            Container.Bind<DefaultEffectSettings>().FromScriptableObjectResource("DefaultEffectSettings").AsSingle();
            CreatePlayer();
            CreateEnemy();
        }

        private void CreatePlayer()
        {
            string PLAYER = "Player_Variant";
            Container.BindFactory<PlayerInject, PlayerInject.FactoryEnemyInject>()
                .FromComponentInNewPrefabResource(PLAYER);
        }

        private void CreateEnemy()
        {
            string ENEMY = "Enemy_Variant";
            Container.BindFactory<EnemyInject, EnemyInject.FactoryEnemyInject>()
                .FromComponentInNewPrefabResource(ENEMY);
        }
    }
}