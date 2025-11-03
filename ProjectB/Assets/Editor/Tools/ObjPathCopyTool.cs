using UnityEditor;
using UnityEngine;

public class ObjPathCopyTool : ScriptableObject
{
    [MenuItem("Tools/复制选中物体的路径 %Q")]
    static void CopyPath()
    {
        Object[] objs = Selection.objects;
        if (objs.Length < 1)
            return;

        GameObject obj = objs[0] as GameObject;
        if (!obj)
            return;

        string path = obj.name;
        Transform parent = obj.transform.parent;
        while (parent)
        {
            if (!parent.parent)
            {
                break;
            }
            path = string.Format("{0}/{1}", parent.name, path);
            parent = parent.parent;
        }

        Debug.Log(path);
        CopyString(path);
    }

    //将字符串赋值到剪切板
    static void CopyString(string str)
    {
        TextEditor te = new TextEditor();
        te.text = str;
        te.SelectAll();
        te.Copy();
    }
}