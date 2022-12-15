using GameItemSystem;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class RuntimeAssetsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // единственный вариант, где юзать Resources будет ОК
            
            var gameItems = Resources.Load<GameItems>(GameItems.ResourcesPath());
            
            Container.Bind<GameItems>()
                .FromInstance(gameItems)
                .AsSingle();
        }
    }
}