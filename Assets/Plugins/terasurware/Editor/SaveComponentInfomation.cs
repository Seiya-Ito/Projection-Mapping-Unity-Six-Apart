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
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Save component infomation.
/// </summary>
[InitializeOnLoad]
public class SaveComponentInfomation
{


	static bool isRecording = false;
	static Dictionary<int, Dictionary<string, object>> saveObjectDic = new Dictionary<int, Dictionary<string, object>> ();

	static SaveComponentInfomation ()
	{
		EditorApplication.playmodeStateChanged = () => 
		{
			if( IsCheckParameter == false)
				return;

			if (isRecording) {
				foreach (MonoBehaviour component  in GameObject.FindObjectsOfType(typeof(MonoBehaviour))) {
					if (EditorApplication.isPlaying) {
						SerializeComponent (component);
					} else {
						DeserializeComponent (component);
					}
				}
			}
			
			isRecording = EditorApplication.isPlaying;
			if (isRecording == false && !EditorApplication.isPlaying) {
				saveObjectDic.Clear ();
			}
		};
	}
	
	private static void SerializeComponent (MonoBehaviour component)
	{
		var dic = new Dictionary<string, object> ();
		var type = component.GetType ();
		foreach (var field in type.GetFields()) {
			if (field.GetCustomAttributes (typeof(PersistentAmongPlayModeAttribute), true).Length != 0) {
				dic.Add (field.Name, field.GetValue (component));
			}
		}
		saveObjectDic.Add (component.GetInstanceID (), dic);
	}

	private static void DeserializeComponent (MonoBehaviour component)
	{
		var type = component.GetType ();
		if(! saveObjectDic.ContainsKey(component.GetInstanceID ()))
			return;
		var dict = saveObjectDic [component.GetInstanceID ()];
		foreach (var field in type.GetFields()) {
			if (field.GetCustomAttributes (typeof(PersistentAmongPlayModeAttribute), true).Length != 0) {
				field.SetValue (component, dict [field.Name]);
			}
		}
	}

	[PreferenceItem("check property")] 
	static void ExampleOnGUI ()
	{
		IsCheckParameter = GUILayout.Toggle(IsCheckParameter, "isCheckParam");
	}

	private static readonly string checkParam = "SaveComponentInfomation checkParam";
	static bool IsCheckParameter {
		get {
			string value = EditorUserSettings.GetConfigValue (checkParam);
			return!string.IsNullOrEmpty (value) && value.Equals ("True");
		}
		set {
			EditorUserSettings.SetConfigValue (checkParam, value.ToString ());
		}
	}
}