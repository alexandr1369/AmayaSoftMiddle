using UnityEngine;
using Zenject;

namespace Installers
{
    public class HomeSceneMainCameraInstaller : MonoInstaller
    {
        [field: SerializeField] private Camera Camera { get; set; }
        
        public override void InstallBindings()
        {
            Container.Bind<Camera>()
                .FromInstance(Camera)
                .AsSingle();
        }
    }
}