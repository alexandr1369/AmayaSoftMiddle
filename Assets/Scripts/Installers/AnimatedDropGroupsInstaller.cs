using GameItemSystem;
using GameItemSystem.DropGroup;
using Zenject;

namespace Installers
{
    public class AnimatedDropGroupsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var gameItems = Container.Resolve<GameItems>();
            var animatedDropGroups = gameItems.GetAssets<AnimatedDropGroup>();
            var emptyAnimatedDropGroup = gameItems.GetAssets<EmptyAnimatedDropGroup>();
            
            animatedDropGroups.ForEach(dropGroup => Container.QueueForInject(dropGroup));
            emptyAnimatedDropGroup.ForEach(dropGroup => Container.QueueForInject(dropGroup));
        }
    }
}