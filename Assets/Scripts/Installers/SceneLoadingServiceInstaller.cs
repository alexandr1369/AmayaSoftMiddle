using LoadingSystem.Loading;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class SceneLoadingServiceInstaller : MonoInstaller
    {
        [field: SerializeField] private SceneLoadingService SceneLoadingService { get; set; }

        public override void InstallBindings()
        {
            Container.Bind<SceneLoadingService>()
                .FromInstance(SceneLoadingService)
                .AsSingle(); 
        }
    }
}