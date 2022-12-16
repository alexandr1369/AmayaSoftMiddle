using Location;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class DragDropManagernstaller : MonoInstaller
    {
        [field: SerializeField] private DragDropManager DragDropManager { get; set; }
        
        public override void InstallBindings()
        {
            Container.Bind<DragDropManager>()
                .FromInstance(DragDropManager)
                .AsSingle();
        }
    }
}