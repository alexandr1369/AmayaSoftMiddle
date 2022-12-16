using Location;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class TouchesServiceInstaller : MonoInstaller
    {
        [field: SerializeField] private TouchesService TouchesService { get; set; }
        
        public override void InstallBindings()
        {
            Container.Bind<TouchesService>()
                .FromInstance(TouchesService)
                .AsSingle();
        }
    }
}