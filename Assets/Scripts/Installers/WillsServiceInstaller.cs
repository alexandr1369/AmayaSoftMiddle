using Location.Character.Will;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class WillsServiceInstaller : MonoInstaller
    {
        [field: SerializeField] private WillsPool WillsPool { get; set; }
        
        public override void InstallBindings()
        {
            BindPool();
            BindFactory();
            BindWillsService();
        }

        private void BindPool()
        {
            Container.Bind<WillsPool>()
                .FromInstance(WillsPool)
                .AsSingle();
        }

        private void BindFactory()
        {
            Container.Bind<Utils.IFactory<Will>>()
                .To<WillsFactory>()
                .AsSingle();
        }

        private void BindWillsService()
        {
            Container.Bind<WillsService>()
                .AsSingle();
        }
    }
}