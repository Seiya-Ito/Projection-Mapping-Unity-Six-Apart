using UnityEngine;
using System.Collections;

public class HSliceMove2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		iTween.MoveTo(this.gameObject, iTween.Hash ('y', -0.175f, "delay", 0, "time", 1.0f, "easeType", "easeInQuart", "oncomplete", "OnComplete"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnComplete()
	{
		Destroy(this.gameObject);
	}
}
