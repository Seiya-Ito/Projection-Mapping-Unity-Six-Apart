using UnityEngine;
using System.Collections;

public class ForciblyChange : MonoBehaviour {

	public float LIMIT;
	private float timer;

	// Use this for initialization
	void Start () {
		timer = LIMIT;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if(timer <= 0) {
			SceneManager.Instance.NextScene();
		}
	}
}
