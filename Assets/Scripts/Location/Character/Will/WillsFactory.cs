using Zenject;

namespace Location.Character.Will
{
    public class WillsFactory : Utils.IFactory<Will>
    {
        private WillsPool _pool;
        
        [Inject]
        private void Construct(WillsPool pool) => _pool = pool;

        public Will Create() => _pool.Spawn();
    }
}