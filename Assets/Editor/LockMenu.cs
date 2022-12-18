using System.Reflection;
using UnityEditor;

namespace Editor
{
    public class LockMenu : EditorWindow
    {
        [MenuItem("Zorg/Tools/Toggle Inspector Lock %l")] // Ctrl + L
        public static void ToggleInspectorLock()
        {
            var inspectorToBeLocked = mouseOverWindow;
            var projectBrowserType = Assembly.GetAssembly(typeof(UnityEditor.Editor))
                .GetType("UnityEditor.ProjectBrowser");
            var inspectorWindowType = Assembly.GetAssembly(typeof(UnityEditor.Editor))
                .GetType("UnityEditor.InspectorWindow");
            
            PropertyInfo propertyInfo;
            if (inspectorToBeLocked.GetType() == projectBrowserType)
            {
                propertyInfo = projectBrowserType
                    .GetProperty("isLocked", 
                        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            }
            else if (inspectorToBeLocked.GetType() == inspectorWindowType)
            {
                propertyInfo = inspectorWindowType.GetProperty("isLocked");
            }
            else
            {
                return;
            }
            
            var value = propertyInfo == null || (bool) propertyInfo.GetValue(inspectorToBeLocked, null);
            propertyInfo?.SetValue(inspectorToBeLocked, !value, null);
            inspectorToBeLocked.Repaint();
        }
    }
}