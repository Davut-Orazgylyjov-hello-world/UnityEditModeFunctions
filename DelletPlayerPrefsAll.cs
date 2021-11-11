using UnityEngine;
using UnityEditor;
 
public class DelletPlayerPrefsAll : EditorWindow
{
    [MenuItem("Window/DelletPlayerPrefsAll")]
    public static void ShowWindow()
    {
        GetWindow<DelletPlayerPrefsAll>("DelletPlayerPrefsAll");
    }
 
    private void OnGUI()
    {
        if (GUILayout.Button("Run Function"))
        {
            FunctionToRun();
        }
    }
 
    private void FunctionToRun()
    {
      PlayerPrefs.DeleteAll();
      PlayerPrefs.Save();
    }
}
