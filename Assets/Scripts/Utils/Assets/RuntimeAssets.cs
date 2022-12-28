using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils.Assets
{
    public abstract class RuntimeAssets<T> : ScriptableObject where T : Object
    {
        [field: SerializeField] protected List<RuntimeAsset<T>> Assets { get; set; }

        public static string ResourcesPath() => $"RuntimeAssets/{typeof(T).Name}Collection";

        public TA GetAsset<TA>(string assetName = null) where TA : T
        {
            foreach (var asset in Assets)
                if (asset.Asset.GetType() == typeof(TA) && (assetName == null || assetName.Equals(asset.Asset.name)))
                    return asset.Asset as TA;

            return null;
        }

        public List<TA> GetAssets<TA>() where TA : T => 
            Assets.FindAll(t => t.Asset.GetType() == typeof(TA))
                .Select(t => t.Asset).Cast<TA>().ToList();

        public virtual void ValidateAssets()
        {
        }
    }
}