using Zenject;

namespace _Core.Scripts.Units.Player.PlayerInject
{
    public class PlayerInject : Player
    {
        public class FactoryEnemyInject : PlaceholderFactory<PlayerInject> { }
    }
}