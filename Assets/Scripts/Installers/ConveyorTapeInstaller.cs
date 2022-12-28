using Location.ConveyorTape;
using Location.ConveyorTape.Item;
using UnityEngine;
using Utils;
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
            Container.Bind<Pool<ConveyorTapeItem>>()
                .To<ConveyorTapeItemsPool>()
                .FromInstance(ItemsPool)
                .AsSingle();
        }

        private void BindFactory()
        {
            Container.Bind<Utils.IFactory<IConveyorTapeItem>>()
                .To<ConveyorTapeItemsFactory>()
                .AsSingle();
        }

        private void BindConveyorTape()
        {
            Container.Bind<IConveyorTape>()
                .To<ConveyorTape>()
                .FromInstance(ConveyorTape)
                .AsSingle();
        }
    }
}