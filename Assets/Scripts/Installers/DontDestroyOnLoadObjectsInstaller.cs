using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class DontDestroyOnLoadObjectsInstaller : MonoInstaller
    {
        [field: SerializeField] private AudioService AudioServicePrefab { get; set; }
        
        private IAudioService _audioService;
        
        public override void InstallBindings()
        {
            LoadData();
            BindAudioService();
            SetDontDestroyOnLoadObjects();
        }

        private void LoadData()
        {
            _audioService = Container.InstantiatePrefabForComponent<IAudioService>(AudioServicePrefab);
            ((AudioService)_audioService).transform.parent = null;
        }

        private void BindAudioService()
        {
            Container.BindInterfacesAndSelfTo<AudioService>()
                .FromComponentOn(((AudioService)_audioService).gameObject)
                .AsSingle();
        }

        private void SetDontDestroyOnLoadObjects()
        {
            var objects = new List<GameObject>
            {
                ((AudioService)_audioService).gameObject
            };
            
            objects.ForEach(DontDestroyOnLoad);
        }
    }
}