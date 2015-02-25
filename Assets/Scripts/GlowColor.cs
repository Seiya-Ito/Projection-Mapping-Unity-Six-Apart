using UnityEngine;
using System.Collections;

public class GlowColor : MonoBehaviour {

	private float timer;
	private const float CHANGE_RATE =  1.0f;
	private float r;
	private float g;
	private float b;
	private float a;
	private int step;
	private int count;

	// Use this for initialization
	void Start () {
		timer = CHANGE_RATE;
		r = 0f;
		g = 0f;
		b = 0f;
		a = 0f;
		step = 0;
		count = 0;
	}
	
	// Update is called once per frame
	void Update () {
		ChangeColor();
		timer -= Time.deltaTime;
		if(timer <= 0) {
			timer = CHANGE_RATE;
			step++;
			Debug.Log(count);
			count = 0;
		}
		count++;
	}

	void ChangeColor() {
		float depth = (CHANGE_RATE-timer)/CHANGE_RATE;
		switch(step) {
			case 1:
				r = depth;
				break;
			case 2:
				g = depth;
				break;
			case 3:
				b = depth;
				break;
			case 5:
				r = 1.0f-depth;
				break;
			case 6:
				g = 1.0f-depth;
				break;
			case 7:
				b = 1.0f-depth;
				break;
			default :
				break;
		}
		this.renderer.material.color = new Color(r, g, b, a);
	}
}
