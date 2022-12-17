using Location.Character.Will;
using UnityEngine;
using Zenject;

namespace Location.Character
{
    public class Character : MonoBehaviour
    {
        [field: SerializeField] private CharacterConfig Config { get; set; }
        [field: SerializeField] private Transform WillSpawnPoint { get; set; }
        [field: SerializeField] public Transform Mouth { get; private set; }
        
        public Will.Will Will { get; private set; }
        
        private CharactersInteractService _interactService;
        private WillsService _willsService;
        
        [Inject]
        private void Construct(CharactersInteractService interactService, WillsService willsService)
        {
            _interactService = interactService;
            _willsService = willsService;
        }

        private void Start()
        {
            _interactService.Add(this);
            Will = _willsService.GetWill();
            Will.transform.parent = transform;
            Will.transform.position = WillSpawnPoint.position;
        }

        public bool IsInteracting(Transform item) => 
            Vector2.Distance(Mouth.position, item.position) <= Config.MaxInteractDistance;

        private void OnDestroy() => _interactService.Remove(this);
    }
}