using UnityEngine;
using System.Collections;

public class ShakeLight : MonoBehaviour {

	public GameObject ball1;
	public GameObject ball2;

	public GameObject cube1;
	public GameObject cube2;
	public GameObject cube3;

	Vector3[] movepath, movepath2, movepath3, movepath4;

	// Use this for initialization
	void Start () {
		movepath = new Vector3[6];
		movepath2 = new Vector3[6];
		movepath3 = new Vector3[6];
		movepath4 = new Vector3[6];
		for (int i=0 ; i<5 ; ++i) {
			movepath[i].Set(0.4062767f-i*0.18f, Random.Range(-0.1f,0.1f), 0.0f);
			
			movepath2[i].Set(-0.4062767f+i*0.18f, Random.Range(-0.1f,0.1f), 0.0f);
		}
		movepath[5].Set(-0.406276f, 0.0f, 0.0f);
		movepath2[5].Set(0.406276f, 0.0f, 0.0f);

		/*for (int i=0; i<5; i++) {
			movepath3[i].Set(0.4062767f-i*0.08f, Random.Range(-0.1f,0.1f), 0.0f);
			
			movepath4[i].Set(-0.4062767f+i*0.08f, Random.Range(-0.1f,0.1f), 0.0f);
		}
		movepath3[5].Set(0.0f, 0.0f, 0.0f);
		movepath4[5].Set(0.0f, 0.0f, 0.0f);
		*/
		iTween.MoveTo(ball1, iTween.Hash("path",movepath,"time",2,"easetype",iTween.EaseType.linear,"oncomplete","completeBall1","oncompletetarget", gameObject));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void completeBall1() {
		Destroy(ball1);
		ball2 = Instantiate(Resources.Load("Prefabs/LightBall"), new Vector3(-0.406276f, 0,0), Quaternion.Euler(0, 0, 0)) as GameObject;
		//ball2.renderer.material.color = new Color(0.0f, 0.0f, 0.0f, 0);
		iTween.MoveTo(ball2, iTween.Hash("path",movepath2,"time",2,"easetype",iTween.EaseType.linear,"onstart","startBall2","oncomplete","completeBall2","oncompletetarget", gameObject));
	}

	void startBall2() {
		ball2.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	}

	void completeBall2() {
		ball1 = Instantiate(Resources.Load("Prefabs/LightBall"), new Vector3(-0.406276f, 0,0), Quaternion.Euler(0, 0, 0)) as GameObject;
		ball2.transform.position = new Vector3(0.406276f, 0,0);
		iTween.MoveTo(ball1, iTween.Hash("position",Vector3.zero,"time",0.2,"delay",0,"easetype",iTween.EaseType.easeOutSine,"oncomplete","completeBall3","oncompletetarget", gameObject));
		iTween.MoveTo(ball2, iTween.Hash("position",Vector3.zero,"time",0.2,"delay",0,"easetype",iTween.EaseType.easeOutSine,"oncomplete","completeBall3","oncompletetarget", gameObject));
	}

	void completeBall3() {
		iTween.ScaleTo(ball1, iTween.Hash("x", 1, "y", 1, "time", 0.5f,"easetype",iTween.EaseType.linear,"oncomplete","completeBall4","oncompletetarget", gameObject));
		/*cube1.renderer.material.color = Color.white;
		cube2.renderer.material.color = Color.white;
		cube3.renderer.material.color = Color.white;*/

	}

	void completeBall4() {
		Destroy (ball1);
		Destroy (ball2);
		cube1.renderer.material.color = Color.white;
		cube2.renderer.material.color = Color.white;
		cube3.renderer.material.color = Color.white;
		SceneManager.Instance.NextScene();
	}
}
