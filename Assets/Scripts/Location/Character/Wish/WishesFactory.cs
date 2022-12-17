using Zenject;

namespace Location.Character.Wish
{
    public class WishesFactory : Utils.IFactory<Wish>
    {
        private WishesPool _pool;
        
        [Inject]
        private void Construct(WishesPool pool) => _pool = pool;

        public Wish Create() => _pool.Spawn();
    }
}