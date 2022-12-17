using Cysharp.Threading.Tasks;
using UnityEngine;

namespace LoadingSystem.Loading.Operations
{
    public class RootLoadingSequence : LoadingSequence
    {
        private const float LOADING_DURATION = 1f;
        
        private readonly CanvasGroup _loadingScreenCanvas;
        private bool _loadingInProgress;

        public RootLoadingSequence(CanvasGroup loadingScreenCanvas) =>
            _loadingScreenCanvas = loadingScreenCanvas;

        public override async UniTask Load()
        {
            if (_loadingInProgress)
                return;

            _loadingInProgress = true;

            await ShowLoadingScreen();

            await base.Load();

            await HideLoadingScreen();
            
            _loadingInProgress = false;
        }

        private async UniTask ShowLoadingScreen() => await FadeLoadingScreen(1f);

        private async UniTask HideLoadingScreen() => await FadeLoadingScreen(0f);

        private async UniTask FadeLoadingScreen(float targetValue)
        {
            var startValue = _loadingScreenCanvas.alpha;
            var time = 0f;
            
            while (time < LOADING_DURATION)
            {
                time += Time.deltaTime;
                _loadingScreenCanvas.alpha = Mathf.Lerp(startValue, targetValue, time / LOADING_DURATION);
                
                await UniTask.Yield();
            }
            
            _loadingScreenCanvas.alpha = targetValue;
        }
    }
}