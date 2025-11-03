using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
 
[CustomEditor(typeof(GameObject))]
public class PreviewPrefabEditor : Editor
{
    private Editor m_GameObjectInspector;
    private MethodInfo m_OnHeaderGUI;
    private MethodInfo m_ShouldHideOpenButton;
 
    Editor reflectorGameObjectEditor
    {
        get
        {
            return m_GameObjectInspector;
        }
    }
 
 
    bool ValidObject()
    {
        string assetPath = GetAssetPath(target);
        return File.Exists(assetPath);
    }
 
    public override bool HasPreviewGUI()
    {
        if (!ValidObject())
            return reflectorGameObjectEditor.HasPreviewGUI();
 
        return true;
    }
 
    public override void OnPreviewGUI(Rect r, GUIStyle background)
    {
 
        if (!ValidObject())
        {
            reflectorGameObjectEditor.OnPreviewGUI(r, background);
            return;
        }

        GUI.DrawTexture(r, AssetDatabase.LoadAssetAtPath<Texture2D>(GetAssetPath(target)));
    }
 
    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        if (!ValidObject())
            return reflectorGameObjectEditor.RenderStaticPreview(assetPath, subAssets, width, height);

        Texture2D tex = new Texture2D(width, height, TextureFormat.ARGB32, false);
        EditorUtility.CopySerialized(AssetDatabase.LoadAssetAtPath<Texture2D>(GetAssetPath(target)), tex);

        return tex;
    }
 
    public override void DrawPreview(Rect previewArea)
    {
        reflectorGameObjectEditor.DrawPreview(previewArea);
    }
 
    public override void OnInspectorGUI()
    {
        reflectorGameObjectEditor.OnInspectorGUI();
    }
 
    public override string GetInfoString()
    {
        return reflectorGameObjectEditor.GetInfoString();
    }
 
    public override GUIContent GetPreviewTitle()
    {
        return reflectorGameObjectEditor.GetPreviewTitle();
    }
 
    public override void OnInteractivePreviewGUI(Rect r, GUIStyle background)
    {
        reflectorGameObjectEditor.OnInteractivePreviewGUI(r, background);
    }
 
 
    public override void OnPreviewSettings()
    {
        reflectorGameObjectEditor.OnPreviewSettings();
    }
 
    public override void ReloadPreviewInstances()
    {
        reflectorGameObjectEditor.ReloadPreviewInstances();
    }
 
    void OnEnable()
    {
        System.Type gameObjectorInspectorType = typeof(Editor).Assembly.GetType("UnityEditor.GameObjectInspector");
        m_OnHeaderGUI = gameObjectorInspectorType.GetMethod("OnHeaderGUI",
            BindingFlags.NonPublic | BindingFlags.Instance);
        m_GameObjectInspector = Editor.CreateEditor(target, gameObjectorInspectorType);
    }
 
    void OnDisable()
    {
        if (m_GameObjectInspector!=null)
            DestroyImmediate(m_GameObjectInspector);
        m_GameObjectInspector = null;
    }
 
    protected override void OnHeaderGUI()
    {
        if (m_OnHeaderGUI != null)
        {
            m_OnHeaderGUI.Invoke(m_GameObjectInspector, null);
        }
    }
 
    public override bool RequiresConstantRepaint()
    {
        return reflectorGameObjectEditor.RequiresConstantRepaint();
    }
 
    public override bool UseDefaultMargins()
    {
        return reflectorGameObjectEditor.UseDefaultMargins();
    }
 
    protected override bool ShouldHideOpenButton()
    {
        return (bool)m_ShouldHideOpenButton.Invoke(m_GameObjectInspector, null);
    }
 
    public static string GetAssetPath(Object obj)
    {
        const string cachePreviewPath = "Editor/UI_Snapshot";

        string guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(obj));


        if (string.IsNullOrEmpty(guid))
        {
            //Debug.LogError("选择的目标不是一个预制体");
            return null;
        }

        string rel_pathname = Path.Combine(cachePreviewPath, guid + ".png");
        string pathname = Path.Combine("Assets", rel_pathname);
        return pathname;
    }

    [MenuItem("Assets/Create/创建 UI Snapshot", false, -100)]
    public static void CreatePreview()
    {
        foreach(var targetGameObject in Selection.gameObjects)
        {
            if (targetGameObject.GetComponent<RectTransform>() == null && targetGameObject.GetComponentInChildren<RectTransform>() == null)
            {
                continue;
            }

            string pathname = GetAssetPath(targetGameObject);

            var preview = GetAssetPreview(targetGameObject);
            SaveTexture2D(preview as Texture2D, pathname);

            {

                AssetDatabase.ImportAsset(pathname);
                AssetDatabase.Refresh();

                TextureImporter Importer = AssetImporter.GetAtPath(pathname) as TextureImporter;
                Importer.textureType = TextureImporterType.GUI;
                TextureImporterPlatformSettings setting = Importer.GetDefaultPlatformTextureSettings();
                setting.format = TextureImporterFormat.RGBA32;
                setting.textureCompression = TextureImporterCompression.Uncompressed;
                Importer.SetPlatformTextureSettings(setting);
                Importer.mipmapEnabled = false;
                Importer.isReadable = true;

                AssetDatabase.ImportAsset(pathname);
                AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(targetGameObject));
                AssetDatabase.Refresh();
            }
        }

        
    }
 
 
    public static Texture GetAssetPreview(GameObject obj)
    {
        GameObject canvas_obj = null;
        GameObject clone = GameObject.Instantiate(obj);
        Transform cloneTransform = clone.transform;
 
        GameObject cameraObj = new GameObject("render camera");
        Camera renderCamera = cameraObj.AddComponent<Camera>();
        renderCamera.backgroundColor = new Color(0.8f, 0.8f, 0.8f, 0);
        renderCamera.clearFlags = CameraClearFlags.Color;
        renderCamera.cameraType = CameraType.SceneView;
        renderCamera.cullingMask = 1 << 21;
        renderCamera.nearClipPlane = -100;
        renderCamera.farClipPlane = 100;
        

        bool isUINode = false;
        if (cloneTransform is RectTransform)
        {
            if (cloneTransform.GetComponent<Canvas>() == null)
            {
                //如果是UGUI节点的话就要把它们放在Canvas下了
                canvas_obj = new GameObject("render canvas", typeof(Canvas));
                Canvas canvas = canvas_obj.GetComponent<Canvas>();
                cloneTransform.SetParent(canvas_obj.transform);
                cloneTransform.localPosition = Vector3.zero;
                //canvas_obj.transform.position = new Vector3(-1000, -1000, -1000);
                canvas_obj.layer = 21;//放在21层，摄像机也只渲染此层的，避免混入了奇怪的东西
                canvas.renderMode = RenderMode.ScreenSpaceCamera;
                canvas.worldCamera = renderCamera;

                var canvasScaler = canvas_obj.AddComponent<CanvasScaler>();
                canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                canvasScaler.referenceResolution = new Vector2(1920, 1080);
                canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
                canvasScaler.matchWidthOrHeight = 1;
            }
            else
            {
                cloneTransform.GetComponent<Canvas>().worldCamera = renderCamera;
            }

            isUINode = true;
        }
        else
            cloneTransform.position = new Vector3(-1000, -1000, -1000);
 
        Transform[] all = clone.GetComponentsInChildren<Transform>();
        foreach (Transform trans in all)
        {
            trans.gameObject.layer = 21;
        }
 
        Bounds bounds = GetBounds(clone, renderCamera);
        Vector3 Min = bounds.min;
        Vector3 Max = bounds.max;
 
 
 
        if (isUINode)
        {
            cameraObj.transform.position = new Vector3(0, 0, -10);
            //Vector3 center = new Vector3(cloneTransform.position.x, (Max.y + Min.y) / 2f, cloneTransform.position.z);
            cameraObj.transform.LookAt(Vector3.zero);
 
            renderCamera.orthographic = true;
            
            float width = Max.x - Min.x;
            float height = Max.y - Min.y;
            float max_camera_size = width > height ? width : height;
            Vector2 scaleDir = width / height > 1920.0f / 1080.0f ? new Vector2(1, 0) : new Vector2(0, 1);
            

            Vector2 scaleF = new Vector2(1920 / max_camera_size, 1080 / max_camera_size) * scaleDir;
            if (cloneTransform.GetComponent<Canvas>() == null)
            {
                renderCamera.orthographicSize = 100;
                cloneTransform.localScale = new Vector3(scaleF.x + scaleF.y, scaleF.x + scaleF.y, 1);
            }
            else
            {
                renderCamera.orthographicSize = max_camera_size;
                cloneTransform.localScale = Vector3.one;
            }
        }
        else
        {
            cameraObj.transform.position = new Vector3((Max.x + Min.x) / 2f, (Max.y + Min.y) / 2f, Max.z + (Max.z - Min.z));
            Vector3 center = new Vector3(cloneTransform.position.x, (Max.y + Min.y) / 2f, cloneTransform.position.z);
            cameraObj.transform.LookAt(center);
 
            int angle = (int)(Mathf.Atan2((Max.y - Min.y) / 2, (Max.z - Min.z)) * 180 / 3.1415f * 2);
            renderCamera.fieldOfView = angle;
        }

        RenderTexture texture = new RenderTexture(800, 600, 0, RenderTextureFormat.ARGB32);
        renderCamera.targetTexture = texture;
 
        var tex = RTImage(renderCamera);

        Object.DestroyImmediate(clone);
        Object.DestroyImmediate(canvas_obj);
        Object.DestroyImmediate(cameraObj);
 
 
        return tex;
    }
 
    static Texture2D RTImage(Camera camera)
    {
        // The Render Texture in RenderTexture.active is the one
        // that will be read by ReadPixels.
        var currentRT = RenderTexture.active;
        RenderTexture.active = camera.targetTexture;
 
        // Render the camera's view.
        //camera.Render();
 
        camera.Render();
        // Make a new texture and read the active Render Texture into it.
        Texture2D image = new Texture2D(800, 600, TextureFormat.ARGB32, false);
        image.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
        image.Apply();
 
        // Replace the original active Render Texture.
        RenderTexture.active = currentRT;
        return image;
    }
 
    public static Bounds GetBounds(GameObject obj, Camera camera)
    {
        Vector3 Min = new Vector3(999999, 999999, 999999);
        Vector3 Max = new Vector3(-999999, -999999, -999999);
        MeshRenderer[] renders = obj.GetComponentsInChildren<MeshRenderer>();
        if (renders.Length > 0)
        {
            for (int i = 0; i < renders.Length; i++)
            {
                if (renders[i].bounds.min.x < Min.x)
                    Min.x = renders[i].bounds.min.x;
                if (renders[i].bounds.min.y < Min.y)
                    Min.y = renders[i].bounds.min.y;
                if (renders[i].bounds.min.z < Min.z)
                    Min.z = renders[i].bounds.min.z;
 
                if (renders[i].bounds.max.x > Max.x)
                    Max.x = renders[i].bounds.max.x;
                if (renders[i].bounds.max.y > Max.y)
                    Max.y = renders[i].bounds.max.y;
                if (renders[i].bounds.max.z > Max.z)
                    Max.z = renders[i].bounds.max.z;
            }
        }
        else
        {
            RectTransform[] rectTrans = obj.GetComponentsInChildren<RectTransform>();
            Vector3[] corner = new Vector3[4];
            for (int i = 0; i < rectTrans.Length; i++)
            {
                //获取节点的四个角的世界坐标，分别按顺序为左下左上，右上右下
                rectTrans[i].GetWorldCorners(corner);
                if (corner[0].x < Min.x)
                    Min.x = corner[0].x;
                if (corner[0].y < Min.y)
                    Min.y = corner[0].y;
                if (corner[0].z < Min.z)
                    Min.z = corner[0].z;
 
                if (corner[2].x > Max.x)
                    Max.x = corner[2].x;
                if (corner[2].y > Max.y)
                    Max.y = corner[2].y;
                if (corner[2].z > Max.z)
                    Max.z = corner[2].z;
            }

            if(camera != null)
            {
                Min = camera.WorldToScreenPoint(Min);
                Max = camera.WorldToScreenPoint(Max);
            }
        }
 
        Vector3 center = (Min + Max) / 2;
        Vector3 size = new Vector3(Max.x - Min.x, Max.y - Min.y, Max.z - Min.z);
        return new Bounds(center, size);
    }
 
 
    public static bool SaveTexture2D(Texture2D png, string save_file_name)
    {
        byte[] bytes = TrimAlpha(png).EncodeToPNG();
        string directory = Path.GetDirectoryName(save_file_name);
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);
        File.WriteAllBytes(save_file_name, bytes);
 
        return true;
    }

    private static Texture2D TrimAlpha(Texture2D oldTex)
    {
        Color32[] pixels = oldTex.GetPixels32();

        int xmin = oldTex.width;
        int xmax = 0;
        int ymin = oldTex.height;
        int ymax = 0;
        int oldWidth = oldTex.width;
        int oldHeight = oldTex.height;

        for (int y = 0, yw = oldHeight; y < yw; ++y)
        {
            for (int x = 0, xw = oldWidth; x < xw; ++x)
            {
                Color32 c = pixels[y * xw + x];

                if (c.a != 0)
                {
                    if (y < ymin) ymin = y;
                    if (y > ymax) ymax = y;
                    if (x < xmin) xmin = x;
                    if (x > xmax) xmax = x;
                }
            }
        }

        int newWidth = (xmax - xmin) + 1;
        int newHeight = (ymax - ymin) + 1;

        if (newWidth > 0 && newHeight > 0)
        {
            if(newWidth == oldWidth && newHeight == oldHeight)
            {
                return oldTex;
            }
            else
            {
                Color32[] newPixels = new Color32[newWidth * newHeight];
                for (int y = 0; y < newHeight; ++y)
                {
                    for (int x = 0; x < newWidth; ++x)
                    {
                        int newIndex = y * newWidth + x;
                        int oldIndex = (ymin + y) * oldWidth + (xmin + x);
                        newPixels[newIndex] = pixels[oldIndex];
                    }
                }

                Texture2D newTex = new Texture2D(newWidth, newHeight, TextureFormat.ARGB32, false);
                newTex.SetPixels32(newPixels);
                newTex.Apply();
                return newTex;
            }
        }
        else
        {
            return oldTex;
        }
    }
}