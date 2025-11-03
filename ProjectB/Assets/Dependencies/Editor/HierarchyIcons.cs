using System;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class HierarchyIcons
{
    private static readonly EditorApplication.HierarchyWindowItemCallback hiearchyItemCallback;

    static HierarchyIcons()
    {
        HierarchyIcons.hiearchyItemCallback = new EditorApplication.HierarchyWindowItemCallback(HierarchyIcons.DrawHierarchyIcon);
        EditorApplication.hierarchyWindowItemOnGUI = (EditorApplication.HierarchyWindowItemCallback)Delegate.Combine(
            EditorApplication.hierarchyWindowItemOnGUI,
            HierarchyIcons.hiearchyItemCallback);

        EditorApplication.projectWindowItemOnGUI = ProjectWindowItemOnGUI;
    }

    private static void ProjectWindowItemOnGUI(string guid, Rect selectionRect)
    {
        
        string prefabRootPath = AssetDatabase.GUIDToAssetPath(guid);
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabRootPath);
        if (prefab != null)
        {
            string assetPath = PreviewPrefabEditor.GetAssetPath(prefab);
            Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);
            if (tex != null)
            {
                if (selectionRect.height < 20)
                {
                    float size = 28;
                    Rect rect = new Rect(selectionRect.x + selectionRect.width - size, selectionRect.y, size, selectionRect.height);
                    GUI.DrawTexture(rect, tex);
                }
            }
        }
    }

    private static void DrawHierarchyIcon(int instanceID, Rect selectionRect)
    {
        GameObject gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if(gameObject != null)
        {
            string prefabRootPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(gameObject);
            if (!string.IsNullOrEmpty(prefabRootPath))
            {
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabRootPath);
                if (prefab != null)
                {
                    bool isLegal = false;
                    if (prefab.GetComponent<Canvas>() == null)
                    {
                        isLegal = true;
                    }
                    else
                    {
                        if (gameObject.GetComponent<Canvas>() != null)
                        {
                            isLegal = true;
                        }
                    }

                    if (isLegal)
                    {
                        string assetPath = PreviewPrefabEditor.GetAssetPath(prefab);
                        Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);
                        if (tex != null)
                        {
                            float size = 28;
                            Rect rect = new Rect(selectionRect.x + selectionRect.width - size, selectionRect.y, size, selectionRect.height);
                            GUI.DrawTexture(rect, tex);
                        }
                    }
                }
            }
        }
    }
}