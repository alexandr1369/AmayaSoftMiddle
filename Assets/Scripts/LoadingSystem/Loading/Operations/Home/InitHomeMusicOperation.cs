using Cysharp.Threading.Tasks;

namespace LoadingSystem.Loading.Operations.Home
{
    public class InitHomeMusicOperation : LoadingOperation
    {
        private readonly AudioService _audioService;
        
        public InitHomeMusicOperation(AudioService audioService) => _audioService = audioService;

        public override async UniTask Load()
        {
            _audioService.PreloadHomeAudio();
            _audioService.PlayHomeMusic();
         
            await UniTask.Yield();
        }
    }
}