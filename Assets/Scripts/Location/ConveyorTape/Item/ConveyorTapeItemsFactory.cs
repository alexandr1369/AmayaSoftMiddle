using Utils;
using Zenject;

namespace Location.ConveyorTape.Item
{
    public class ConveyorTapeItemsFactory : Utils.IFactory<IConveyorTapeItem>
    {
        private Pool<ConveyorTapeItem> _pool;
        
        [Inject]
        private void Construct(Pool<ConveyorTapeItem> pool) => _pool = pool;

        public IConveyorTapeItem Create()
        {
            var item = _pool.Spawn();
            item.OnCollected += () => _pool.Despawn(item);
            
            return item;
        }
    }
}