using Editor.AutoItems;
using GameItemSystem;
using UnityEditor;

namespace Editor
{
    public static class RuntimeAssetsReloader
    {
        private static readonly string[] GameItemsFolderPaths = { $"Assets/Resources/Items" };
        
        [InitializeOnEnterPlayMode]
        [MenuItem("AmayaSoft/Reload Runtime Assets")]
        public static void ReloadRuntimeAssets()
        {
            RuntimeAssetsScanner<GameItems, GameItem>.UpdateAssetsReferences(GameItemsFolderPaths);
        }
    }
}