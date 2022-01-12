using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorObjViewer : EditorWindow {

    private Vector2 scrollPos;

    public object obj { get; set; }
    public object objParent { get; set; }

    public static void Init() {
        EditorObjViewer window = EditorWindow.GetWindow<EditorObjViewer>();
        //window.minSize = new Vector2(200, 30);
        window.Show();
    }

    private int compareMethodInfos(MethodInfo methodInfo1, MethodInfo methodInfo2) {
         return methodInfo1.Name.CompareTo(methodInfo2.Name);
    }

    private int compareProps(PropertyInfo p1, PropertyInfo p2) {
         return p1.Name.CompareTo(p2.Name);
    }

    private void OnGUI() {
        GUILayout.BeginVertical();
        
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.LabelField("Parent instance:\n" + objParent.GetType().Name, GUILayout.MinHeight(50));
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        EditorGUILayout.LabelField("ToString():\n" + obj.ToString(), GUILayout.MinHeight(50));
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        scrollPos = GUILayout.BeginScrollView(scrollPos,true,true,GUILayout.MinHeight(200),GUILayout.MaxHeight(1000),GUILayout.ExpandHeight(true));

        var methodInfos = obj.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public);
        Array.Sort(methodInfos, compareMethodInfos);

        foreach (MethodInfo methodInfo in methodInfos)
        {
            if (methodInfo.GetParameters().Length > 0) {
                continue;
            }

            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(methodInfo.Name);
            
            if (GUILayout.Button("+")) {
                var window = (EditorObjViewer)ScriptableObject.CreateInstance(typeof(EditorObjViewer));
                window.minSize = new Vector2(200, 30);
                window.obj = methodInfo.Invoke(obj, null);
                window.objParent = obj;
                window.Show();
            }
            
            GUILayout.EndHorizontal();
        }
        
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.LabelField("Properties");
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        
        var props = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
        Array.Sort(props, compareProps);

        foreach (var prop in props)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(prop.Name);
            
            if (GUILayout.Button("+")) {
                var window = (EditorObjViewer)ScriptableObject.CreateInstance(typeof(EditorObjViewer));
                window.minSize = new Vector2(200, 30);
                window.obj = prop.GetValue(obj);
                window.objParent = obj;
                window.Show();
            }
            
            GUILayout.EndHorizontal();
        }

        GUILayout.EndScrollView ();
        GUILayout.EndVertical();
    }
}

public class EditorDebugLog : EditorWindow
{

    private UnityEngine.Object obj;

    private Vector2 scrollPos;

    private string inptText = "";

    private int compareMethodInfos(MethodInfo methodInfo1, MethodInfo methodInfo2) {
         return methodInfo1.Name.CompareTo(methodInfo2.Name);
    }

    private int compareProps(PropertyInfo p1, PropertyInfo p2) {
         return p1.Name.CompareTo(p2.Name);
    }

    [MenuItem("Screenshot/Text")]
    private static void Init()
    {
        EditorDebugLog window = EditorWindow.GetWindow<EditorDebugLog>();
        window.Show();
    }

    private void OnEnable() {}

    private void OnGUI()
    {
        GUILayout.BeginVertical();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        obj = EditorGUILayout.ObjectField(obj, typeof(UnityEngine.Object), true);
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        if (obj == null) {
            return;
        }
        
        scrollPos = GUILayout.BeginScrollView(scrollPos,true,true,GUILayout.MinHeight(200),GUILayout.MaxHeight(1000),GUILayout.ExpandHeight(true));

        var methodInfos = obj.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public);
        Array.Sort(methodInfos, compareMethodInfos);

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.LabelField("Methods");
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        foreach (MethodInfo methodInfo in methodInfos)
        {
            if (methodInfo.GetParameters().Length > 0) {
                continue;
            }

            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(methodInfo.Name);
            
            if (GUILayout.Button("+")) {
                var window = (EditorObjViewer)ScriptableObject.CreateInstance(typeof(EditorObjViewer));
                window.minSize = new Vector2(200, 30);
                window.obj = methodInfo.Invoke(obj, null);
                window.objParent = obj;
                window.Show();
            }
            
            GUILayout.EndHorizontal();
        }

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.LabelField("Properties");
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        
        var props = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
        Array.Sort(props, compareProps);

        foreach (var prop in props)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(prop.Name);
            
            if (GUILayout.Button("+")) {
                var window = (EditorObjViewer)ScriptableObject.CreateInstance(typeof(EditorObjViewer));
                window.minSize = new Vector2(200, 30);
                window.obj = prop.GetValue(obj);
                window.objParent = obj;
                window.Show();
            }
            
            GUILayout.EndHorizontal();
        }

        GUILayout.EndScrollView ();
        GUILayout.EndVertical();

    }
        
}