using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils.Assets
{
    [Serializable]
    public class RuntimeAsset<T> where T : Object
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public T Asset { get; private set; }

        public RuntimeAsset(T item)
        {
            Name = item.name;
            Asset = item;
        }
    }
}