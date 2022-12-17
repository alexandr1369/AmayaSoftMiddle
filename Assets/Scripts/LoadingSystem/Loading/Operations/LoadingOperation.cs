using Cysharp.Threading.Tasks;

namespace LoadingSystem.Loading.Operations
{
    public abstract class LoadingOperation
    {
        public abstract UniTask Load();

        /// <summary>
        /// Вес операции, нужен для визуализации загрузки
        /// </summary>
        /// <returns></returns>
        public virtual float Weight() => 1f;
    }
}