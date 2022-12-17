using Location.Character;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class CharactersInteractServiceInstaller : MonoInstaller
    {
        [field: SerializeField] private CharactersInteractService Service { get; set; }

        public override void InstallBindings()
        {
            Container.Bind<CharactersInteractService>()
                .FromInstance(Service)
                .AsSingle();
        }
    }
}