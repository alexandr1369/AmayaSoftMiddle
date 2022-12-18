using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class DontDestroyOnLoadObjectsInstaller : MonoInstaller
    {
        [field: SerializeField] private AudioService AudioServicePrefab { get; set; }
        
        private AudioService _audioService;
        
        public override void InstallBindings()
        {
            LoadData();
            BindAudioService();
            SetDontDestroyOnLoadObjects();
        }

        private void LoadData()
        {
            _audioService = Container.InstantiatePrefabForComponent<AudioService>(AudioServicePrefab);
            _audioService.transform.parent = null;
        }

        private void BindAudioService()
        {
            Container.Bind<AudioService>()
                .FromComponentOn(_audioService.gameObject)
                .AsSingle();
        }

        private void SetDontDestroyOnLoadObjects()
        {
            var objects = new List<GameObject>
            {
                _audioService.gameObject
            };
            
            objects.ForEach(DontDestroyOnLoad);
        }
    }
}