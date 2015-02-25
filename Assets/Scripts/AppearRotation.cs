using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AppearRotation : MonoBehaviour {
	
	private const int NUM = 320;
	
	public List<IEvent> eventList = new List<IEvent> ();
	
	public MyObject[] objs = new MyObject[NUM];
	private int scene;
	
	// Use this for initialization
	void Start () {
		for (int i=0 ; i<NUM ; ++i) {
			objs[i] = new MyObject();
			objs[i].obj = Instantiate(Resources.Load("Prefabs/boxes"), new Vector3(0,-0.1625f+0.001f*i,0), Quaternion.Euler(90, 45, 0)) as GameObject;
		}
		
		eventList.Add (new RotationEvent(this));
		//eventList.Add (new ScaleEvent(this));
		//eventList.Add (new MovingEvent (this));
		//eventList.Add (new RotationEvent (this));
		
		/*MovingEvent me = new MovingEvent (this);
		me.setDirection (new Vector3 (-0.5f, -0.5f, 0));
		me.setDistance(0.32f*2);
		eventList.Add (me);*/
	}
	
	// Update is called once per frame
	void Update () {
		if (eventList.Count > 0) {
			eventList[0].action();
		} else {

		}
	}
	
	void nextState() {
		for (int i=0; i<NUM; ++i) {
			objs[i].frame = 0;
		}
		eventList.RemoveAt (0);
	}
	
	public interface IEvent {
		void action();
	}
	
	class RotationEvent : IEvent {
		
		public AppearRotation scene;
		public MyObject[] objs;
		
		private int[] frame;

		bool flag = false;

		public RotationEvent(AppearRotation scene) {
			this.scene = scene;
			this.objs = scene.objs;
			this.frame = new int[this.objs.Length];
			for (int i=0; i<NUM; ++i) {
				objs[i].obj.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0);
			}
		}
		
		public void action() {
			const int wait = 1;
			const int time = 200;//1600;
			const float degree = 180.0f;//180.0f;
			const float delta = degree / (time-wait+1);
			for (int i=0; i<NUM; ++i) {
				if (frame [i] < 0) {
					continue;
				} else if (frame[i]++ > time*2) {
					frame[i] = -1;
				} else if (frame[i] > wait) {
					objs[i].obj.transform.Rotate(0, 0, delta);
					objs[i].obj.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
				} else {
					break;
				}
			}
			if (frame[NUM - 1] > time*2) {
				SceneManager.Instance.NextScene();
			}

			if (!flag) {
				flag = true;
				action();
				flag = false;
			}
		}
	}
	
	class MovingEvent : IEvent {
		
		public AppearRotation scene;
		public MyObject[] objs;
		
		float distance = 0.2474874f;//0.8f;
		Vector3 dir = new Vector3 (0.5f, 0.5f, 0);
		
		public MovingEvent(AppearRotation scene, float distance = 0.32f) {
			this.scene = scene;
			this.objs = scene.objs;
			this.distance = distance;
			
		}
		
		public void action() {
			int wait = 3;
			int time = 10;
			float delta = distance / time;
			for (int i=0; i<NUM; ++i) {
				if (objs [i].frame++ > time) {
					continue;
				} else {
					objs[i].obj.transform.Translate(dir * delta);
					if (objs [i].frame < wait)
						break;
				}
			}
			if (objs [NUM - 1].frame > time) {
				scene.nextState ();
			}
		}
		
		public void setDistance(float distance) {
			this.distance = distance;
		}
		
		public void setDirection(Vector3 dir) {
			this.dir = dir;
		}
	}
	
	class ScaleEvent : IEvent {
		
		public AppearRotation scene;
		public MyObject[] objs;
		
		private int[] frame;
		
		private int pos;
		
		public ScaleEvent(AppearRotation scene) {
			this.scene = scene;
			this.objs = scene.objs;
			this.frame = new int[scene.objs.Length];
		}
		
		public void action() {
			// const int wait = 1;
			const int time = 130;
			for (int i=0; i<NUM; ++i) {
				float scale = (Mathf.Cos (-pos / 20.0f + i / 20.0f) + 1.0f) / 10.0f + 0.8f;
				if (scale > 0.95f && frame[i] >= 0) {
					if (frame[i]++ > time) {
						frame[i] = -1;
						objs[i].obj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
						// objs[i].obj.renderer.material.color = Color.white;
					} else{
						// objs[i].obj.renderer.material.color = ColorHSV.FromHsv((i+pos/10)%360, 255, 255);
					}
				}
				if (frame[i] > 0) {
					objs[i].obj.transform.localScale = new Vector3(scale, scale, 1.0f);
				} else if (frame[i] == 0) {
					break;
				}
			}
			++pos;
			
			if (frame [NUM - 1] > time) {
				scene.nextState ();
			}
		}
		
	}
	
	class AppearEvent : IEvent {
		
		public AppearRotation scene;
		public MyObject[] objs;
		
		private int[] frame;
		
		private int counter;
		
		public AppearEvent(AppearRotation scene) {
			this.scene = scene;
			this.objs = scene.objs;
			this.frame = new int[scene.objs.Length];
		}
		
		public void action() {
			
			for (int i=0; i<NUM; ++i) {
				
			}
			
		}
		
	}

}

public class MyObject {
	public GameObject obj;
	public int frame;
	public MyObject() {
		frame = 0;
	}
}
