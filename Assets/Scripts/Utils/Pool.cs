using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Utils
{
    public abstract class Pool<T> : MonoBehaviour where T : MonoBehaviour
    {
        private DiContainer _diContainer;
        [field: SerializeField] protected T Item { get; private set; }
        [field: SerializeField] protected int Count { get; private set; }

        [Inject]
        private void Construct(DiContainer diContainer) => _diContainer = diContainer;

        private List<T> Items { get; set; }

        protected virtual void Awake()
        {
            Items = new List<T>();
            
            for (var i = 0; i < Count; i++)
            {
                var item = _diContainer.InstantiatePrefabForComponent<T>(Item, transform);
                Despawn(item);
            }
        }

        public T Spawn()
        {
            var item = Items[^1];
            Items.Remove(item);
            item.gameObject.SetActive(true);
            
            return item;
        }

        public void Despawn(T item)
        {
            Items.Add(item);
            item.transform.parent = transform;
            item.gameObject.SetActive(false);
        }
    }
}