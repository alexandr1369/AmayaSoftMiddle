using System.Collections.Generic;
using GameItemSystem;
using LoadingSystem.Loading.Operations.Home;
using StateSystem;
using StateSystem.CounteditemState;
using UnityEngine;
using Zenject;

namespace UI
{
    public class HudItems : MonoBehaviour
    {
        [field: SerializeField] public Canvas Canvas { get; private set; }
        [field: SerializeField] private HudItem HudItemPrefab { get; set; }
        [field: SerializeField] private List<GameItem> Items { get; set; }

        private readonly List<HudItem> _hudItems = new();
        private GameController _gameController;

        [Inject]
        private void Construct(GameController gameController, HomeSceneLoadingContext context)
        {
            _gameController = gameController;
            context.HudItems = this;
        }

        private void Start() => InitItemsStates();

        private void InitItemsStates()
        {
            foreach (var item in Items)
            {
                if(_hudItems.Find(t => t.Item == item))
                    continue;
                
                var hudItem = Instantiate(HudItemPrefab, transform);
                hudItem.Init(item);
                _hudItems.Add(hudItem);
                
                var hudItemState = _gameController.State.GetIntCountedItemStateForItem(hudItem.Item);
                hudItemState.OnChanged += IntCountedItemChangedAction;
                IntCountedItemChangedAction(hudItemState);
            }
        }
        
        public Transform GetItemTransform(GameItem item) => _hudItems.Find(t => t.Item == item)?.transform;

        private void IntCountedItemChangedAction(IntCountedItemState state)
        {
            var hudItem = _hudItems.Find(t => t.Item.name == state.Id);

            if(!hudItem)
                return;
            
            hudItem.SetAmountText(state.Count);
            hudItem.gameObject.SetActive(state.Count > 0);
        }
    }
}