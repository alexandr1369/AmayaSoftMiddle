using Editor.AutoItems;
using GameItemSystem;
using UnityEditor;

namespace Editor
{
    public static class RuntimeAssetsReloader
    {
        private static readonly string[] GameItemsFolderPaths = { $"Assets/Resources/Items" };
        
        [InitializeOnEnterPlayMode]
        [MenuItem("Zorg/Reload Runtime Assets")]
        public static void ReloadRuntimeAssets()
        {
            RuntimeAssetsScanner<GameItems, GameItem>.UpdateAssetsReferences(GameItemsFolderPaths);
        }
    }
}