using LoadingSystem.Loading.Operations.Home;
using Location.Character;
using Location.ConveyorTape.Item.Movement;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;
using Zenject;

namespace Location.ConveyorTape.Item
{
    public class ConveyorTapeItemDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler 
    {
        [field: SerializeField] private ConveyorTapeItem Item { get; set; }
        
        public HomeSceneLoadingContext Context { get; set; }
        public Vector3? PointerPosition { get; private set; }
        
        private InputDelegate _inputDelegate;
        private CharactersInteractService _service;
        private int _normalOrderInLayer;

        [Inject]
        private void Construct(
            HomeSceneLoadingContext context,
            InputDelegate inputDelegate,
            CharactersInteractService service)
        {
            Context = context;
            _inputDelegate = inputDelegate;
            _service = service;
        }

        private void OnEnable() => PointerPosition = null;

        private void Update()
        {
            if(!PointerPosition.HasValue || transform.position == PointerPosition)
                return;
            
            transform.position = Vector2.Lerp(transform.position, 
                PointerPosition.Value, Time.deltaTime * Item.Config.DraggingVelocity);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if(!_inputDelegate.HasPermission(this))
                return;
            
            Item.SetMoveBehaviour(new DraggingConveyorTapeItemMoveBehaviour());
            _normalOrderInLayer = Item.SpriteRenderer.sortingOrder;
            Item.SpriteRenderer.sortingOrder = Item.Config.DraggingOrderInLayer;
            Item.PlayDraggingAnimation(true);
            Context.AudioService.PlayLocalFx(Item.AudioSource, Context.AudioService.ClickClip);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if(Context == null || !_inputDelegate.HasPermission(this))
                return;
            
            PointerPosition = Context.HomeSceneCamera.Camera.ScreenToWorldPoint(eventData.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if(!_inputDelegate.HasPermission(this))
                return;
            
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
            PointerPosition = null;
        }
    }
}