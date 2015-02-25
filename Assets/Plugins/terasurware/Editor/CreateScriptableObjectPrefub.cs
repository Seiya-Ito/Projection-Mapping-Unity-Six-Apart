/*
 The MIT License (MIT)

Copyright (c) 2013 yamamura tatsuhiko

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

/// <summary>
// ScriptableObjectをプレハブとして出力する汎用スクリプト
/// </summary>
// <remarks>
// 指定したScriptableObjectをプレハブに変換する。
// 1.Editorフォルダ下にCreateScriptableObjectPrefub.csを配置
// 2.ScriptableObjectのファイルを選択して右クリック→Create ScriptableObjectを選択
// </remarks>
public class CreateScriptableObjectPrefub
{
	readonly static string[] labels = {"Data", "ScriptableObject"};
	
	[MenuItem("Assets/Create ScriptableObject")]
	static void Crate ()
	{
		foreach (Object selectedObject in Selection.objects) {
			// get path
			string path = getSavePath (selectedObject);
			// create instance
			ScriptableObject obj = ScriptableObject.CreateInstance (selectedObject.name);
			AssetDatabase.CreateAsset (obj, path);
			// add label
			ScriptableObject sobj = AssetDatabase.LoadAssetAtPath (path, typeof(ScriptableObject)) as ScriptableObject;
			AssetDatabase.SetLabels (sobj, labels);
			EditorUtility.SetDirty (sobj);
		}
	}
	static string getSavePath (Object selectedObject)
	{
		string objectName = selectedObject.name;
		string dirPath = Path.GetDirectoryName (AssetDatabase.GetAssetPath (selectedObject));
		string path = string.Format ("{0}/{1}.asset", dirPath, objectName);
		if (File.Exists (path))
		for (int i=1;; i++) {
			path = string.Format ("{0}/{1}({2}).asset", dirPath, objectName, i);
			if (! File.Exists (path))
				break;
		}
		return path;
	}
}