using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class TrimCanvasEditor : EditorWindow
{
    [MenuItem("游戏工具/处理图片尺寸")]
    private static void GetWindow()
    {
        TrimCanvasEditor window = GetWindow<TrimCanvasEditor>(true, "修改图片尺寸", true);
        window.Show(); 

    }
    private SerializedObject serializedObject;
    private SerializedProperty texture2DListProperty;
    private Vector2Int trimSize;
    private string savePath;
    [SerializeField]
    private List<Texture2D> texture2DList;
    private Vector2 selectScrollPosition = Vector2.zero;

    private bool isSelf;
    private string width;
    private string height;

    private void OnEnable()
    {
        texture2DList = new List<Texture2D>();
        serializedObject = new SerializedObject(this);
        texture2DListProperty = serializedObject.FindProperty("texture2DList");
    }

    private void OnDisable()
    {
        serializedObject.Dispose();
    }

    private void OnGUI()
    {
        isSelf =  EditorGUILayout.Toggle("是否自定义尺寸", isSelf);
        width = EditorGUILayout.TextField("设置宽：", width);
        height = EditorGUILayout.TextField("设置高：", height);

        selectScrollPosition = EditorGUILayout.BeginScrollView(selectScrollPosition, EditorStyles.helpBox);
        {
            EditorGUILayout.PropertyField(texture2DListProperty, new GUIContent("选择图集(选中多个图片拖拽进来)"), true);
        }
        EditorGUILayout.EndScrollView();

        EditorGUILayout.Space();

        if (GUILayout.Button("保存", GUILayout.MinHeight(30), GUILayout.MinWidth(20)))
        {
            for (int i = 0; i < texture2DListProperty.arraySize; i++)
            {
                SerializedProperty texProperty = texture2DListProperty.GetArrayElementAtIndex(i);

                Texture2D item = texProperty.objectReferenceValue as Texture2D;

                if (isSelf)
                {
                    if (string.IsNullOrEmpty(width) || string.IsNullOrEmpty(height))
                    {
                        Debug.Log("<size=15><Color=green>请先设置宽高</Color></size>");
                    }
                    else
                    {
                        trimSize.x = int.Parse(width);
                        trimSize.y = int.Parse(height);

                    }
                }
                else
                {
                    if ((int)item.width % 4 != 0 && (int)item.width >= 4)
                    {
                        trimSize.x = (int)item.width + (4 - item.width % 4);
                    }
                    else
                    {
                        trimSize.x = item.width;
                    }
                    if ((int)item.height % 4 != 0 && (int)item.height >= 4)
                    {
                        trimSize.y = (int)item.height + (4 - item.height % 4);
                    }
                    else
                    {
                        trimSize.y = item.height;
                    }
                }

                
                Texture2D texture = new Texture2D(trimSize.x, trimSize.y, TextureFormat.RGBA32, item.streamingMipmaps);

                TextureImporter textureImporter = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(item)) as TextureImporter;
                textureImporter.isReadable = true;
                textureImporter.SaveAndReimport();

                int widthDifference = texture.width - item.width;
                int heightDifference = texture.height - item.height;

                widthDifference /= 2;
                heightDifference /= 2;

                Color[] itemPixels = item.GetPixels();
                Color[] texturePixels = texture.GetPixels();

                for (int j = 0; j < texturePixels.Length; j++)
                {
                    int x = j % texture.width;
                    int y = j / texture.width;

                    x -= widthDifference;
                    y -= heightDifference;

                    int itemIndex = y * item.width + x;

                    if (x >= 0 && y >= 0 && x < item.width && y < item.height && itemIndex < itemPixels.Length)
                    {
                        texturePixels[j] = itemPixels[itemIndex];
                    }
                    else
                    {
                        texturePixels[j] = new Color(0, 0, 0, 0);
                    }
                }

                texture.SetPixels(0, 0, texture.width, texture.height, texturePixels);
                texture.Apply();

                string contents = Application.dataPath;

                byte[] bytes = texture.EncodeToPNG();
                if (!Directory.Exists(contents))
                    Directory.CreateDirectory(contents);

                savePath = UnityEditor.AssetDatabase.GetAssetPath(item);
                FileStream file = File.Create(savePath);
                BinaryWriter writer = new BinaryWriter(file);
                writer.Write(bytes);
                file.Close();

                AssetDatabase.ImportAsset(savePath);

                textureImporter = AssetImporter.GetAtPath(savePath) as TextureImporter;
                textureImporter.isReadable = false;
                textureImporter.SaveAndReimport();

                EditorUtility.DisplayProgressBar("正在处理 : ", item.name, (float)(i + 1) / texture2DListProperty.arraySize);
            }
            Debug.Log("所有图片处理成功：共 <size=15><Color=green>" + texture2DListProperty.arraySize +"</Color></size>");
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();
        }
    }
}
