using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LoadingSystem.Loading.Operations
{
    public class LoadSceneOperation : LoadingOperation
    {
        private readonly string _sceneName;
        private readonly LoadSceneMode _mode;
        private AsyncOperation _operation;

        public LoadSceneOperation(string sceneName, LoadSceneMode mode)
        {
            _sceneName = sceneName;
            _mode = mode;
        }

        public override async UniTask Load()
        {
            if (_operation != null)
                return;
            
            _operation = SceneManager.LoadSceneAsync(_sceneName, _mode);
            
            await _operation;

            Debug.Log("[LoadSceneOperation]: " + _sceneName + " has been loaded in " + Time.realtimeSinceStartup);
        }

        public override float Weight() => _operation?.progress ?? 1;
    }
}