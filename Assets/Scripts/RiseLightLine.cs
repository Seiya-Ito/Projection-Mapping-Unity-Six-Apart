using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RiseLightLine : MonoBehaviour {
	
	public List<IEvent> eventList = new List<IEvent> ();
	
	// Use this for initialization
	void Start () {
		eventList.Add(new UpPlaneEvent(this));
	}
	
	// Update is called once per frame
	void Update () {
		if (eventList.Count > 0)
			eventList[0].action();
	}
	
	void nextState() {
		eventList.RemoveAt (0);
	}
	
	public interface IEvent {
		void action();
	}
	
	class UpPlaneEvent : IEvent {
		
		private RiseLightLine scene;
		
		private GameObject[] obj;
		
		private int frame;
		
		public UpPlaneEvent(RiseLightLine scene) {
			this.scene = scene;
		}
		
		void createBar(int idx, bool start = true) {
			GameObject obj;
			float pos = 0.2474874f-idx*0.2474874f;
			if (start) {
				obj = Instantiate(Resources.Load("Prefabs/HSlice"), new Vector3(pos, -0.1625f,0), Quaternion.Euler(0, 45, 0)) as GameObject;
			} else {
				obj = Instantiate(Resources.Load("Prefabs/HSlice2"), new Vector3(pos, 0.1625f,0), Quaternion.Euler(0, 45, 0)) as GameObject;
			}
			obj.renderer.material.color = new Color(1.0f, 0, 0, 1.0f);
		}
		
		public void action() {
			
			if (frame++ == 0) {
				createBar(0);
			} else if (frame == 30) {
				createBar(1);
			} else if (frame == 60) {
				createBar(2);
			} else if (90 < frame && frame < 330) {
				if (frame % 60 ==0) {
					createBar(1);
				} else if ((frame + 20) % 60 ==0) {
					createBar(0);
				} else if ((frame + 40) % 60 ==0) {
					createBar(2);
				}
			} else if (frame == 360 || frame == 390 || frame == 420 || frame == 450) {
				createBar(0);
				createBar(1);
				createBar(2);
			} else if (frame == 480) {
				createBar(0, false);
				createBar(1, false);
				createBar(2, false);
			} else if (frame == 560) {
				SceneManager.Instance.NextScene();
			}
		}
		
	}
}
