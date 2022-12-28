using LoadingSystem.Loading;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class SceneLoadingServiceInstaller : MonoInstaller
    {
        [field: SerializeField] private SceneLoadingService SceneLoadingServicePrefab { get; set; }

        public override void InstallBindings()
        {
            var service = Container.InstantiatePrefabForComponent<SceneLoadingService>(SceneLoadingServicePrefab);
            
            Container.Bind<SceneLoadingService>()
                .FromComponentsOn(service.gameObject)
                .AsSingle(); 
        }
    }
}