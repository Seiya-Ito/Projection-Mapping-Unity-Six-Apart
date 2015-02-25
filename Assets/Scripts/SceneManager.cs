using UnityEngine;
using System.Collections;

public class SceneManager : SingletonMonoBehaviour<SceneManager> {//MonoBehaviour {

	public GameObject mainCamera;
	public GameObject fadeCamera;
	//public AudioClip music;
	private Scene[] scenes;
	private int currentSceneNo;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(mainCamera);
		DontDestroyOnLoad(fadeCamera);

		scenes = new[] {
			// new Scene("GlowCube", KeyCode.S),
			// new Scene("RiseLightLine", KeyCode.Alpha1),
			// new Scene("MoveAndCrashLightBall", KeyCode.Alpha2),
			// new Scene("ColRotate", KeyCode.Alpha3),
			// new Scene("Destroy", KeyCode.Alpha4),
			// new Scene("AppearRotation", KeyCode.Alpha5),
			// new Scene("WaveCube", KeyCode.Alpha6),
			// new Scene("SliceAndCopyCube", KeyCode.Alpha7),
			// new Scene("Slide", KeyCode.Alpha8),
			// new Scene("CheckRotate", KeyCode.Alpha0),
			// new Scene("RawRotate", KeyCode.Q),
			// new Scene("Roll", KeyCode.W),
			// new Scene("BallRotation", KeyCode.E),
			// new Scene("ColorChange", KeyCode.R),
			// new Scene("Finish", KeyCode.F),
			new Scene("01", KeyCode.A),
			new Scene("02", KeyCode.S),
			new Scene("03", KeyCode.D),
			new Scene("04", KeyCode.F),
			new Scene("05", KeyCode.G),
			new Scene("06", KeyCode.H),
			new Scene("07", KeyCode.J),
			new Scene("08", KeyCode.K),
			new Scene("09", KeyCode.L),
			new Scene("10", KeyCode.Semicolon)
		};
		LoadLevel (scenes[currentSceneNo].name);
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Scene scene in scenes) {
			if (Input.GetKeyDown(scene.keycode)) {
				LoadLevel (scene.name);
			}
		}
		/*if (currentSceneNo == scenes.Length - 1) {
			audio.volume -= 0.01f;
		}*/
	}

	public void NextScene() {
		// currentSceneNo = (currentSceneNo + 1) % scenes.Length;
		for (int i = 0; i < scenes.Length; ++i) {
			if (scenes[i].name.Equals(Application.loadedLevelName)) {
				currentSceneNo = (i + 1) % scenes.Length;
				break;
			}
		}
		Application.LoadLevel(scenes[currentSceneNo].name);
		Debug.Log(currentSceneNo);
	}


	public void LoadLevel (string name) {
		
		float time = 1.5f;

		FadeCamera.Instance.FadeOut (time, () =>
		{
			// finish
			Application.LoadLevel(name);

			FadeCamera.Instance.FadeIn (time, () =>
			{
				// finish
				for (int i = 0; i < scenes.Length; ++i) {
					if (scenes[i].name.Equals(name)) {
						currentSceneNo = i;
					}
				}
			});
			
		});
		
	}

	public void LoadLevel (int num) {
		
		float time = 1.5f;

		FadeCamera.Instance.FadeOut (time, () =>
		{
			// finish
			Application.LoadLevel(scenes[num].name);

			FadeCamera.Instance.FadeIn (time, () =>
			{
				// finish
				for (int i = 0; i < scenes.Length; ++i) {
					if (scenes[i].name.Equals(name)) {
						currentSceneNo = i;
					}
				}
			});
			
		});
		
	}

	class Scene {
		public string name;
		public KeyCode keycode;

		public Scene(string name, KeyCode keycode) {
			this.name = name;
			this.keycode = keycode;
		}
	}
}
