using UnityEngine;
using System.Collections;

public class QuadRotation : MonoBehaviour {

	public GameObject Quad;
	public Vector3 Position;
	private const int NUM = 90;
	private int curNum;
	private GameObject[] Quads;
	private float timer;
	private const float SPEED = 1.0f;

	// Use this for initialization
	void Start () {
		curNum = 0;
		Quads = new GameObject[NUM];
		Quads[curNum] = (GameObject)Instantiate(Quad);
		Quads[curNum].transform.Translate(Position);
		curNum++;
		setTimer();
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if(timer <= 0) {
			Quads[curNum] = (GameObject)Instantiate(Quad);
			Quads[curNum].transform.Translate(Position);
			curNum++;
			setTimer();
		}
		for(int i = 0; i < curNum; i++) {
			Quads[i].transform.RotateAround(Vector3.up, 0.3f);
		}
	}

	void setTimer() {
		timer = SPEED;
	}
}
