using Utils;
using Zenject;

namespace Location.Character.Wish
{
    public class WishesFactory : Utils.IFactory<Wish>
    {
        private Pool<Wish> _pool;
        
        [Inject]
        private void Construct(Pool<Wish> pool) => _pool = pool;

        public Wish Create() => _pool.Spawn();
    }
}