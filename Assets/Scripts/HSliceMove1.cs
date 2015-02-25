using UnityEngine;
using System.Collections;

public class HSliceMove1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		iTween.MoveTo(this.gameObject, iTween.Hash ('y', 0.175f, "delay", 0, "time", 0.4f, "easeType", iTween.EaseType.linear, "oncomplete", "OnComplete"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnComplete()
	{
		Destroy(this.gameObject);
	}
}
