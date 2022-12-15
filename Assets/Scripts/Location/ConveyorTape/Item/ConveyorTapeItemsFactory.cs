using Zenject;

namespace Location.ConveyorTape.Item
{
    public class ConveyorTapeItemsFactory : Utils.IFactory<ConveyorTapeItem>
    {
        private ConveyorTapeItemsPool _pool;
        
        [Inject]
        private void Construct(ConveyorTapeItemsPool pool) => _pool = pool;

        public ConveyorTapeItem Create()
        {
            var item = _pool.Spawn();
            item.OnCollected += () => _pool.Despawn(item);
            
            return item;
        }
    }
}