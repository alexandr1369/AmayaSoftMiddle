using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public abstract class Pool<T> : MonoBehaviour where T : MonoBehaviour
    {
        [field: SerializeField] protected T Item { get; private set; }
        [field: SerializeField] protected int Count { get; private set; }

        private List<T> Items { get; set; }

        private void Awake()
        {
            Items = new List<T>();
            
            for (var i = 0; i < Count; i++)
            {
                var item = Instantiate(Item, transform);
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