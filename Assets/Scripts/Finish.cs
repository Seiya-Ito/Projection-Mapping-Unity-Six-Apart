using UnityEngine;
using System.Collections;

public class Finish : MonoBehaviour {

	public float fadeout = 0.005f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Camera.main.audio.volume -= fadeout;
	}
}
