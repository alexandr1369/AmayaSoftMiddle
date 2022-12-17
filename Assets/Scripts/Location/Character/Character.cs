using Location.Character.Wish;
using UnityEngine;
using Zenject;

namespace Location.Character
{
    public class Character : MonoBehaviour
    {
        [field: SerializeField] private CharacterConfig Config { get; set; }
        [field: SerializeField] private Transform WishSpawnPoint { get; set; }
        [field: SerializeField] public Transform Mouth { get; private set; }
        
        public Wish.Wish Wish { get; private set; }
        
        private CharactersInteractService _interactService;
        private WishesService _wishesService;
        
        [Inject]
        private void Construct(CharactersInteractService interactService, WishesService wishesService)
        {
            _interactService = interactService;
            _wishesService = wishesService;
        }

        private void Start()
        {
            _interactService.Add(this);
            Wish = _wishesService.GetWish();
            Wish.transform.parent = transform;
            Wish.transform.position = WishSpawnPoint.position;
        }

        public bool IsInteracting(Transform item) => 
            Vector2.Distance(Mouth.position, item.position) <= Config.MaxInteractDistance;

        private void OnDestroy() => _interactService.Remove(this);
    }
}