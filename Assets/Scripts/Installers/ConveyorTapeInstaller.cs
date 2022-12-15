using Location.ConveyorTape;
using Location.ConveyorTape.Item;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ConveyorTapeInstaller : MonoInstaller
    {
        [field: SerializeField] private ConveyorTape ConveyorTape { get; set; }
        [field: SerializeField] private ConveyorTapeItemsPool ItemsPool { get; set; }
        
        public override void InstallBindings()
        {
            BindPool();
            BindFactory();
            BindConveyorTape();
        }

        private void BindPool()
        {
            Container.Bind<ConveyorTapeItemsPool>()
                .FromInstance(ItemsPool)
                .AsSingle();
        }

        private void BindFactory()
        {
            Container.Bind<Utils.IFactory<ConveyorTapeItem>>()
                .To<ConveyorTapeItemsFactory>()
                .AsSingle();
        }

        private void BindConveyorTape()
        {
            Container.Bind<ConveyorTape>()
                .FromInstance(ConveyorTape)
                .AsSingle();
        }
    }
}