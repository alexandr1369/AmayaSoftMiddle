using Location.Character.Wish;
using UnityEngine;
using Utils;
using Zenject;

namespace Installers
{
    public class WishesServiceInstaller : MonoInstaller
    {
        [field: SerializeField] private WishesPool WishesPool { get; set; }
        
        public override void InstallBindings()
        {
            BindPool();
            BindFactory();
            BindWishesService();
        }

        private void BindPool()
        {
            Container.Bind<Pool<Wish>>()
                .To<WishesPool>()
                .FromInstance(WishesPool)
                .AsSingle();
        }

        private void BindFactory()
        {
            Container.Bind<Utils.IFactory<Wish>>()
                .To<WishesFactory>()
                .AsSingle();
        }

        private void BindWishesService()
        {
            Container.Bind<WishesService>()
                .AsSingle();
        }
    }
}