using UnityEngine;
using System.Collections;

public class BallsRotation : MonoBehaviour {

	public GameObject[] Balls;
	public GameObject[] RndBalls;
	public GameObject[][] BallsChildren;
	public GameObject Ball;
	public Transform sphere;
	
	private int BallsNum;
	private int ChildrenNum;
	private int count;
	private int frame;
	private int step;
	private int fade;
	private int[] RndBallsNum;
	private float timer;
	private float gap;

	private Vector3 BOX_SIZE = new Vector3(0.175f, 0.325f, 0.175f);

	private const float SPHERE_RADIUS = 0.03f;
	private const int FADEOUT = 50;

	// Use this for initialization
	void Start () {

		BallsNum = Balls.Length;
		ChildrenNum = Ball.transform.childCount;
		fade = FADEOUT;
		gap =  (BOX_SIZE.y - SPHERE_RADIUS * 2)/ BallsNum;

		BallsChildren = new GameObject[BallsNum][];
		RndBalls = new GameObject[BallsNum * ChildrenNum];
		
		for(int i=0; i<BallsNum; i++) {
			BallsChildren[i] = new GameObject[ChildrenNum];
			for(int j=0; j<ChildrenNum; j++) {
				BallsChildren[i][j] = Balls[i].transform.GetChild(j).gameObject;
				BallsChildren[i][j].transform.renderer.enabled = false;
				RndBalls[count++] = Balls[i].transform.GetChild(j).gameObject;
			}
		}

		setRandom();
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		switch(step) {
			case 0:
				if(timer <= 0) {
					Intro();
				}
				break;
			case 1:
				if(timer <= 0) {
					animation.Play("BallsRotation");
					step++;
				}
				break;
			case 2:
				if(!animation.isPlaying) {
					step++;
				}
				break;
			case 3:
				if(timer <= 0) {
					Gather();
				}
				break;
			case 4:
				if(timer <= 0) {
					setTimer();
					step++;
				}
				break;
			case 5:
				Burst();
				if(timer <= 0) {
					SceneManager.Instance.NextScene();
				}
				break;
			default:
				break;
		}
	}

	void setRandom() {
		int rnd;
		RndBallsNum = new int[count];
		for(int i=0; i<count; i++) {
			rnd = Random.Range(0,count-1);
			for(int j=0; j<i; j++) {
				if(rnd == RndBallsNum[j]) {
					rnd = Random.Range(0,count-1);
					j=0;
				}
			}
			RndBallsNum[i] = rnd;
		}
	}

	void Intro() {
			if(frame%2==1) {
				RndBalls[RndBallsNum[(frame/2)%count]].renderer.enabled = false;
			} else {
				RndBalls[RndBallsNum[(frame/2)%count]].renderer.enabled = true;
			}
			timer = func_1(frame);
			if(timer <= Time.deltaTime) {
				if(fade > 0) {
					if(frame%2==1) {
						RndBalls[RndBallsNum[(frame/2)%count]].renderer.enabled = false;
					} else {
						RndBalls[RndBallsNum[(frame/2)%count]].renderer.enabled = true;
					}
					fade--;
				}
				else {
					for(int i=0; i<count; i++) {
						RndBalls[i].renderer.enabled = true;
					}
					step++;
					setTimer();
				}
			}
			frame++;
		
	}

	void Gather() {
		for(int i=0; i<count; i++) {
			Transform t = RndBalls[i].transform;
			t.transform.LookAt(sphere);
			t.transform.Translate(new Vector3(0f,0f,0.01f));
			RndBalls[i].transform.LookAt(t);
		}
		step++;
		setTimer(0.005f);
	}

	void Burst() {
		sphere.localScale = new Vector3(0,0,0);
		for(int i=0; i<count; i++) {
			RndBalls[i].transform.Translate(Vector3.forward*Time.deltaTime);
		}
	}

	void setTimer() {
		timer = 1.0f;
	}

	void setTimer(float t) {
		timer = t;
	}

	float func_1(int frame) {
		if(fade == FADEOUT) {
			return 1.0f/(frame+1);
		} else {
			return 0;
		}
	}

}
