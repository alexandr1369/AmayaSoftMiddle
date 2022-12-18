using LoadingSystem.Loading.Operations.Home;
using Location.Character;
using Location.ConveyorTape.Item.Movement;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Location.ConveyorTape.Item
{
    public class ConveyorTapeItemDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler 
    {
        [field: SerializeField] private ConveyorTapeItem Item { get; set; }
        
        private HomeSceneLoadingContext _context;
        private CharactersInteractService _service;
        private Vector3? _pointerPosition;
        private int _normalOrderInLayer;

        [Inject]
        private void Construct(HomeSceneLoadingContext context, CharactersInteractService service)
        {
            _context = context;
            _service = service;
        }

        private void OnEnable() => _pointerPosition = null;

        private void Update()
        {
            if(!_pointerPosition.HasValue || transform.position == _pointerPosition)
                return;
            
            transform.position = Vector2.Lerp(transform.position, 
                _pointerPosition.Value, Time.deltaTime * Item.Config.DraggingVelocity);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Item.SetMoveBehaviour(new DraggingConveyorTapeItemMoveBehaviour());
            _normalOrderInLayer = Item.SpriteRenderer.sortingOrder;
            Item.SpriteRenderer.sortingOrder = Item.Config.DraggingOrderInLayer;
            Item.PlayDraggingAnimation(true);
            _context.AudioService.PlayLocalFx(Item.AudioSource, _context.AudioService.ClickClip);
        }

        public void OnDrag(PointerEventData eventData) => 
            _pointerPosition = _context.HomeSceneCamera.Camera.ScreenToWorldPoint(eventData.position);

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_service.IsInteracting(Item, out var character))
            {
                character.Wish.MakeWishComeTrue();
                Item.PlayInteractionAnimation(character.Mouth.position);
            }
            else
            {
                Item.SetMoveBehaviour(new MovableConveyorTapeItemMoveBehaviour(
                    Item.transform, Item.Config.FallingVelocity));
                Item.PlayDraggingAnimation(false);
            }

            Item.SpriteRenderer.sortingOrder = _normalOrderInLayer;
            _pointerPosition = null;
        }
    }
}