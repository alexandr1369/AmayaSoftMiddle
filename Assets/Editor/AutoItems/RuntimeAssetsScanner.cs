using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;
using Utils.Assets;

namespace Editor.AutoItems
{
    public class RuntimeAssetsScanner<TAssets, TAsset> where TAsset : Object where TAssets : RuntimeAssets<TAsset> 
    {
        protected internal static void UpdateAssetsReferences(string[] foldersToScan)
        {
            Debug.Log("Scanning and updating items..");

            var targets = FindTargetObjects(foldersToScan);
            var packedTargets = PackObjects(targets);

            var assets = LoadRuntimeAssets();
            
            if (!assets)
            {
                assets = CreateRuntimeAssets();
            }
            
            var serializedEditor = new SerializedObject(assets);

            SetTargetsToRuntimeAssets(assets, packedTargets);
            EditorUtility.SetDirty(assets);
            serializedEditor.ApplyModifiedProperties();
            assets.ValidateAssets();
            
            Debug.Log($"{assets.name} has been updated.");
        }

        private static void SetTargetsToRuntimeAssets(TAssets runtimeAssets, IEnumerable<RuntimeAsset<TAsset>> targets)
        {
            var field = typeof(TAssets).GetProperty("Assets", BindingFlags.NonPublic | BindingFlags.Instance);

            if (field != null)
            {
                field.SetValue(runtimeAssets, targets);
            }
        }

        private static void AddAllPrefabs(string[] foldersToScan, List<TAsset> items)
        {
            var guids = AssetDatabase.FindAssets("t:Object", foldersToScan);
            var infoMessageBuilder = new StringBuilder();

            foreach (var guid in guids)
            {
                var objectPath = AssetDatabase.GUIDToAssetPath(guid);
                var allObjects = AssetDatabase.LoadAllAssetsAtPath(objectPath);

                foreach (var obj in allObjects)
                {
                    if (obj is TAsset casted)
                    {
                        items.Add(casted);
                        infoMessageBuilder.Append($"{objectPath}\n");
                    }
                }
            }
            
            infoMessageBuilder.Insert(0, $"Found {items.Count} dialogs:\n");
            
            Debug.Log(infoMessageBuilder.ToString());
        }

        private static void AddAllScriptableObjects(string[] foldersToScan, List<TAsset> items)
        {
            var targetTypeName = typeof(TAsset);
            var guids = AssetDatabase.FindAssets($"t:{targetTypeName}", foldersToScan);
            var infoMessageBuilder = new StringBuilder();
            
            foreach (var t in guids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(t);
                var asset = AssetDatabase.LoadAssetAtPath<TAsset>(assetPath);
                
                if (asset == null)
                {
                    continue;
                }
                
                items.Add(asset);
                infoMessageBuilder.Append($"{assetPath}\n");
            }

            infoMessageBuilder.Insert(0, $"Found {items.Count} items:\n");
            
            Debug.Log(infoMessageBuilder.ToString());
        }

        public static string FullPathToResourcesAsset() => 
            $"Assets/Resources/{RuntimeAssets<TAsset>.ResourcesPath()}.asset";

        private static TAssets LoadRuntimeAssets() => 
            AssetDatabase.LoadAssetAtPath<TAssets>(FullPathToResourcesAsset());

        private static TAssets CreateRuntimeAssets()
        {
            var assets = ScriptableObject.CreateInstance<TAssets>();
            
            AssetDatabase.CreateAsset(assets, FullPathToResourcesAsset());
            AssetDatabase.SaveAssets();

            return assets;
        }

        private static IEnumerable<TAsset> FindTargetObjects(string[] foldersToScan)
        {
            var targetTypeName = typeof(TAsset);
            var items = new List<TAsset>();
            
            Debug.Log($"Scanning folders for {targetTypeName}...");

            if (typeof(ScriptableObject).IsAssignableFrom(typeof(TAsset)))
            {
                AddAllScriptableObjects(foldersToScan, items);
            }
            else if (typeof(MonoBehaviour).IsAssignableFrom(typeof(TAsset)))
            {
                AddAllPrefabs(foldersToScan, items);
            }
            
            return items;
        }

        private static IEnumerable<RuntimeAsset<TAsset>> PackObjects(IEnumerable<TAsset> targetObjects)
        {
            return targetObjects
                .Select(item => new RuntimeAsset<TAsset>(item))
                .ToList();
        }
    }
}