using Cysharp.Threading.Tasks;
using Location.ConveyorTape;

namespace LoadingSystem.Loading.Operations.Home
{
    public class ConveyorTapeBuildingOperation : LoadingOperation
    {
        private readonly HomeSceneLoadingContext _context;

        public ConveyorTapeBuildingOperation(HomeSceneLoadingContext context) => _context = context;

        public override async UniTask Load()
        {
            _context.ConveyorTapeBuildingService.BuildTape();
            
            await UniTask.Yield();

            _context.ConveyorTape.StartConveyorTape();
            
            await UniTask.Yield();
        }
    }
}