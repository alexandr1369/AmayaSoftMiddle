using StateSystem;
using Zenject;

namespace Installers
{
    public class GameStateInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameStateService>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<GameController>()
                .AsSingle();
        }
    }
}