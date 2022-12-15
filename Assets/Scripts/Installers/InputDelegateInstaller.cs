using Utils;
using Zenject;

namespace Installers
{
    public class InputDelegateInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<InputDelegate>()
                .AsSingle();
        }
    }
}