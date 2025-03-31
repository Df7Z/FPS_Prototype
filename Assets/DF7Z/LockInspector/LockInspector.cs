using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
public class InspectorLockTest 
{
    [MenuItem("Hot actions/Toggle Inspector lock %SPACE")]
    static public void ToggleInspectorLock()
    {
        var inspectorType = typeof(Editor).Assembly.GetType("UnityEditor.InspectorWindow");

        var isLocked = inspectorType.GetProperty("isLocked",System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

        var inspectorWindow = EditorWindow.GetWindow(inspectorType);

        var state = isLocked.GetGetMethod().Invoke(inspectorWindow, new object[] { });

        isLocked.GetSetMethod().Invoke(inspectorWindow, new object[] { !(bool)state });
    }
}



public class EditorWindowLock
{

    
    
    private static EditorWindow _activeProject;
 
    
    /*
    [MenuItem("Hot actions/Toggle Lock %SPACE")]
    static void ToggleInspectorLock()
    {
        ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
        ActiveEditorTracker.sharedTracker.ForceRebuild();
    }*/
    
    [MenuItem("Hot actions/Toggle Project Lock %#SPACE")]
    static void ToggleProjectLock()
    {
        if (_activeProject == null)
        {
            Type type = Assembly.GetAssembly(typeof(UnityEditor.Editor)).GetType("UnityEditor.ProjectBrowser");
            Object[] findObjectsOfTypeAll = Resources.FindObjectsOfTypeAll(type);
            _activeProject = (EditorWindow)findObjectsOfTypeAll[0];
        }
      
        if (_activeProject != null && _activeProject.GetType().Name == "ProjectBrowser")
        {
            Type type = Assembly.GetAssembly(typeof(UnityEditor.Editor)).GetType("UnityEditor.ProjectBrowser");
            PropertyInfo propertyInfo = type.GetProperty("isLocked", BindingFlags.Instance | 
                                                                     BindingFlags.NonPublic |
                                                                     BindingFlags.Public);
        
            bool value = (bool)propertyInfo.GetValue(_activeProject, null);
      
            propertyInfo.SetValue(_activeProject, !value, null);
      
            _activeProject.Repaint();
        }
    }
}
#endif