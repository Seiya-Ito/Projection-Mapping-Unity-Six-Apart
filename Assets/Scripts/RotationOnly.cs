using UnityEngine;
using System.Collections;

public class RotationOnly : MonoBehaviour {

	private float f;

	// Use this for initialization
	void Start () {
		f = 0;
		this.transform.localScale = new Vector3(0f,0f,0f);
	}
	
	// Update is called once per frame
	void Update () {
		if(f <= 100) {
			this.transform.localScale = new Vector3(0.05f*f/100f, 0.05f*f/100f, 0.05f*f/100f);
			f+=1;
		} else {
			this.transform.RotateAround(Vector3.up, Mathf.Sin(Time.deltaTime));
		}
		
	}
}
