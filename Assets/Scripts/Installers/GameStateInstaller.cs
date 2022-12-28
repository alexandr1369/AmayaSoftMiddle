using StateSystem;
using Zenject;

namespace Installers
{
    public class GameStateInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameStateService>()
                .To<GameStateService>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<GameController>()
                .AsSingle();
        }
    }
}