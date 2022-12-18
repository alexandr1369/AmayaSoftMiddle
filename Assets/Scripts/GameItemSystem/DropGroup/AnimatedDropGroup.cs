using System;
using Backpack;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using LoadingSystem.Loading.Operations.Home;
using UI;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;
using Sequence = DG.Tweening.Sequence;

namespace GameItemSystem.DropGroup
{
    [CreateAssetMenu(menuName = "AmayaSoft/Object/New Animated Drop Group", fileName = "AnimatedDropGroup")]
    public class AnimatedDropGroup : DropGroup
    {
        private const int MAX_ANIMATION_ITEMS_COUNT = 5;
        
        private const float START_DELAY = .2f;
        private const float MOVEMENT_1_DURATION = .7f;
        private const float MOVEMENT_2_DURATION = .7f;
        private const float INSIDE_UNIT_CIRCLE_MULTIPLIER = 150f;

        [field: SerializeField] private AnimatedItemFx AnimatedItemFxPrefab { get; set; }
        
        private HomeSceneLoadingContext _context;

        [Inject]
        private void Construct(HomeSceneLoadingContext context) => _context = context;

        public void Animate(Vector3 startWorldPosition, Transform container)
        {
            if (Items.Count == 1)
                AnimateSingleItem(startWorldPosition, container);
            else
                AnimateSeveralItems(startWorldPosition, container);
        }

        private async void AnimateSingleItem(Vector3 startWorldPosition, Transform container)
        {
            var item = Items[0];
            var hudItemTransform = await GetHudItemTransform(item.Item);
            var itemEndScreenPosition = hudItemTransform.position;
            var itemsAmount = Items[0].Count < MAX_ANIMATION_ITEMS_COUNT 
                ? Mathf.CeilToInt(Items[0].Count) 
                : Mathf.CeilToInt(MAX_ANIMATION_ITEMS_COUNT);

            for (var i = 0; i < itemsAmount; i++)
                AnimateItem(item.Item, i, startWorldPosition, itemEndScreenPosition, container, () =>
                {
                    var itemCount = item.Count / itemsAmount;
                    var reward = new GameCurrency(item.Item, itemCount);
                    _context.Inventory.Add(reward);
                });
        }

        private async void AnimateSeveralItems(Vector3 startWorldPosition, Transform container)
        {
            for (var i = 0; i < Items.Count; i++)
            {
                var item = Items[i];
                var hudItemTransform = await GetHudItemTransform(item.Item);
                var itemEndScreenPosition = hudItemTransform.position;
                
                AnimateItem(item.Item, i, startWorldPosition, itemEndScreenPosition, container, () =>
                {
                    var itemCount = item.Count;
                    var reward = new GameCurrency(item.Item, itemCount);
                    _context.Inventory.Add(reward);
                });
            }
        }

        private void AnimateItem(
            GameItem item,
            int iterationIndex,
            Vector3 startWorldPosition,
            Vector3 endScreenPosition,
            Transform container,
            Action onComplete = null)
        {
            var movementSequence = DOTween.Sequence();
            var screenPosition = _context.HomeSceneCamera.Camera.WorldToScreenPoint(startWorldPosition);
            
            var itemFx = Instantiate(AnimatedItemFxPrefab, container);
            itemFx.transform.position = screenPosition;
            itemFx.Init(item.Icon);

            var fxSequence = GetMovementSequence(itemFx, iterationIndex, screenPosition, endScreenPosition);
            movementSequence.Join(fxSequence);
            movementSequence.OnComplete(() => onComplete?.Invoke());
            movementSequence.Play();
        }

        private static Sequence GetMovementSequence(
            Component itemFx,
            int iterationIndex,
            Vector3 startScreenPosition,
            Vector3 endScreenPosition)
        {
            var itemFxMovement1 = itemFx.transform
                .DOMove(startScreenPosition + (Vector3)Random.insideUnitCircle * INSIDE_UNIT_CIRCLE_MULTIPLIER,
                    MOVEMENT_1_DURATION)
                .SetDelay(iterationIndex * START_DELAY)
                .SetEase(Ease.OutCubic);
            var itemFxMovement2 = itemFx.transform.DOMove(endScreenPosition, MOVEMENT_2_DURATION)
                .SetEase(Ease.OutCirc);
            
            return DOTween.Sequence()
                .Append(itemFxMovement1)
                .Append(itemFxMovement2)
                .OnKill(() => Destroy(itemFx.gameObject));
        }

        private async UniTask<Transform> GetHudItemTransform(GameItem item)
        {
            var hudItemTransform = _context.HudItems.GetItemTransform(item);

            if (hudItemTransform.gameObject.activeSelf)
                return hudItemTransform;
            
            hudItemTransform.gameObject.SetActive(true);
            
            // <-- awaiting for hud item enabling (if disabled)-->
            await UniTask.Delay(TimeSpan.FromSeconds(.1f), delayTiming: PlayerLoopTiming.Update);
                
            return hudItemTransform;
        }
    }
}