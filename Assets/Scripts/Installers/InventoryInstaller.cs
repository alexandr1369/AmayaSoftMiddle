using Backpack;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class InventoryInstaller : MonoInstaller
    {
        [field: SerializeField] private GameInitConfig GameInitConfig { get; set; }

        public override void InstallBindings()
        {
            Container.Bind<GameInitConfig>()
                .FromInstance(GameInitConfig)
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<Inventory>()
                .AsSingle();
        }
    }
}