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
using System.Collections;
using UnityEditor;
using UnityEngine;
using System.IO;

[InitializeOnLoad]
public class AutoSave
{
	public static readonly string manualSaveKey = "autosave@manualSave";

	static double nextTime = 0;
	static AutoSave ()
	{
		IsManualSave = true;
		EditorApplication.playmodeStateChanged += () =>
		{
			if (IsAutoSave && !EditorApplication.isPlaying && EditorApplication.isPlayingOrWillChangePlaymode) {

				IsManualSave = false;

				if (IsSavePrefab)
					EditorApplication.SaveAssets ();
				if (IsSaveScene) {
					Debug.Log ("save scene " + System.DateTime.Now);
					EditorApplication.SaveScene ();
				}

				IsManualSave = true;
			}
		};

		nextTime = EditorApplication.timeSinceStartup + Interval;
		EditorApplication.update += () =>
		{
			if (nextTime < EditorApplication.timeSinceStartup) {
				nextTime = EditorApplication.timeSinceStartup + Interval;

				IsManualSave = false;

				if (IsSaveSceneTimer && IsAutoSave && !EditorApplication.isPlaying) {
					if (IsSavePrefab)
						EditorApplication.SaveAssets ();
					if (IsSaveScene) {
						Debug.Log ("save scene " + System.DateTime.Now);
						EditorApplication.SaveScene ();
					}
				}

				IsManualSave = true;
			}
		};
	}

	public static bool IsManualSave {
		get {
			return EditorPrefs.GetBool (manualSaveKey);
		}
		private set {
			EditorPrefs.SetBool (manualSaveKey, value);
		}
	}


	private static readonly string autoSave = "auto save";
	static bool IsAutoSave {
		get {
			string value = EditorUserSettings.GetConfigValue (autoSave);
			return!string.IsNullOrEmpty (value) && value.Equals ("True");
		}
		set {
			EditorUserSettings.SetConfigValue (autoSave, value.ToString ());
		}
	}

	private static readonly string autoSavePrefab = "auto save prefab";
	static bool IsSavePrefab {
		get {
			string value = EditorUserSettings.GetConfigValue (autoSavePrefab);
			return!string.IsNullOrEmpty (value) && value.Equals ("True");
		}
		set {
			EditorUserSettings.SetConfigValue (autoSavePrefab, value.ToString ());
		}
	}

	private static readonly string autoSaveScene = "auto save scene";
	static bool IsSaveScene {
		get {
			string value = EditorUserSettings.GetConfigValue (autoSaveScene);
			return!string.IsNullOrEmpty (value) && value.Equals ("True");
		}
		set {
			EditorUserSettings.SetConfigValue (autoSaveScene, value.ToString ());
		}
	}

	private static readonly string autoSaveSceneTimer = "auto save scene timer";
	static bool IsSaveSceneTimer {
		get {
			string value = EditorUserSettings.GetConfigValue (autoSaveSceneTimer);
			return!string.IsNullOrEmpty (value) && value.Equals ("True");
		}
		set {
			EditorUserSettings.SetConfigValue (autoSaveSceneTimer, value.ToString ());
		}
	}

	private static readonly string autoSaveInterval = "save scene interval";
	static int Interval {
		get {

			string value = EditorUserSettings.GetConfigValue (autoSaveInterval);
			if (value == null) {
				value = "60";
			}
			return int.Parse (value);
		}
		set {
			if (value < 60)
				value = 60;
			EditorUserSettings.SetConfigValue (autoSaveInterval, value.ToString ());
		}
	}


	[PreferenceItem("Auto Save")] 
	static void ExampleOnGUI ()
	{
		bool isAutoSave = EditorGUILayout.BeginToggleGroup ("auto save", IsAutoSave);


		IsAutoSave = isAutoSave;
		EditorGUILayout.Space ();

		IsSavePrefab = EditorGUILayout.ToggleLeft ("save prefab", IsSavePrefab);
		IsSaveScene = EditorGUILayout.ToggleLeft ("save scene", IsSaveScene);
		IsSaveSceneTimer = EditorGUILayout.BeginToggleGroup ("save scene interval", IsSaveSceneTimer);
		Interval = EditorGUILayout.IntField ("interval(sec)", Interval);
		EditorGUILayout.EndToggleGroup ();
		EditorGUILayout.EndToggleGroup ();
	}

	[MenuItem("File/Backup/Backup")]
	public static void Backup ()
	{
		string expoertPath = "Backup/" + EditorApplication.currentScene;

		Directory.CreateDirectory (Path.GetDirectoryName (expoertPath));
		byte[] data = File.ReadAllBytes (EditorApplication.currentScene);
		File.WriteAllBytes (expoertPath, data);
	}

	[MenuItem("File/Backup/Rollback")]
	public static void RollBack ()
	{
		string expoertPath = "Backup/" + EditorApplication.currentScene;
		
		byte[] data = File.ReadAllBytes (expoertPath);
		File.WriteAllBytes (EditorApplication.currentScene, data);
		AssetDatabase.Refresh (ImportAssetOptions.Default);
	}

}