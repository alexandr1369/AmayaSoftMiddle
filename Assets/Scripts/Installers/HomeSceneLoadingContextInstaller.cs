using LoadingSystem.Loading.Operations.Home;
using Zenject;

namespace Installers
{
    public class HomeSceneLoadingContextInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<HomeSceneLoadingContext>()
                .AsSingle();
        }
    }
}