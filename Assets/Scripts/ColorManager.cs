using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColorManager : MonoBehaviour {

	public static GameObject[] obj = new GameObject[6];
	private static float[] frame = new float[6];
	public ChangePropaty[] props = new ChangePropaty[6];
	private static Propaty[] currProp = new Propaty[6];

	static Color black = Color.black;
	
	static Color[] colorLabel = new Color[50];


	// Use this for initialization
	void Start () {
		makeColor ();
		instantiates ();
		setProps ();
		for (int i = 0; i < 6; i++) {
			currProp [i] = props[i].getProp ();
		}

	}

	void instantiates(){
		obj[0] = 
			Instantiate(Resources.Load("Prefabs/Plate"), new Vector3(-0.30939f, 0f, -0.0619f), Quaternion.Euler(0, 45, 0))as GameObject;
		obj[1] = 
			Instantiate(Resources.Load("Prefabs/Plate"), new Vector3(-0.18559f, 0f, -0.0619f), Quaternion.Euler(0, -45, 0))as GameObject;
		obj[2] = 
			Instantiate(Resources.Load("Prefabs/Plate"), new Vector3(-0.0619f, 0f, -0.0619f), Quaternion.Euler(0, 45, 0))as GameObject;
		obj[3] =
			Instantiate(Resources.Load("Prefabs/Plate"), new Vector3(0.0619f, 0f, -0.0619f), Quaternion.Euler(0, -45, 0))as GameObject;
		obj[4] =
			Instantiate(Resources.Load("Prefabs/Plate"), new Vector3(0.18559f, 0f, -0.0619f), Quaternion.Euler(0, 45, 0))as GameObject;
		obj[5] =
			Instantiate(Resources.Load("Prefabs/Plate"), new Vector3(0.30939f, 0f, -0.0619f), Quaternion.Euler(0, -45, 0))as GameObject;
	}
	
	void setProps(){

		Color[] colors = new Color[6];

		float speed = 0.03f;

		for (int i = 0; i < 6; i++) {
			props [i] = new ChangePropaty ();
		}

		//from left
		for (int i = 0; i < 42; i++) {
			for(int j = 0; j < 6; j++){
				colors[j] = colorLabel[i+j];
			}
			setAction (speed, colors);
		}

		//from right
		for (int i = 0; i < 42; i++) {
			for(int j = 0; j < 6; j++){
				colors[5-j] = colorLabel[i+j];
			}
			setAction (speed, colors);
		}

		//from side
		for (int i = 0; i < 42; i++) {
			for(int j = 0; j < 3; j++){
				colors[5-j] = colorLabel[i+j];
				colors[j] = colorLabel[i+j];
			}
			setAction (speed, colors);
		}

		//from center
		for (int i = 0; i < 42; i++) {
			for(int j = 0; j < 3; j++){
				colors[j+3] = colorLabel[i+j];
				colors[2-j] = colorLabel[i+j];
			}
			setAction (speed, colors);
		}

		//clearColor (speed);
		//clearColor(5.0f);
	}

	//set all plate "Black".
	void clearColor(float t){
		for (int i = 0; i < 6; i++) {
			props[i].add (t, black);
		}
	}

	void setAction(float t, Color[] col){
		for (int i = 0; i < col.Length; i++) {
			props[i].add (t, col[i]);
		}
	}

	void makeColor(){
		for (int i = 0; i < 6; i++) {
			colorLabel[i] = black;
		}
		for (int i = 0; i < 6; i++) {
			colorLabel[i+6] = new Color(1f,0f,1f/6f*i);
		}
		for (int i = 0; i < 6; i++) {
			colorLabel[i+12] = new Color(1f/6f*(5-i), 0f, 1f);
		}
		for (int i = 0; i < 6; i++) {
			colorLabel[i+18] = new Color(0f, 1f/6f*i, 1f);
		}
		for (int i = 0; i < 6; i++) {
			colorLabel[i+24] = new Color(0f, 1f, 1f/6f*(5-i));
		}
		for (int i = 0; i < 6; i++) {
			colorLabel[i+30] = new Color(1f/6f*i, 1f, 0f);
		}
		for (int i = 0; i < 6; i++) {
			colorLabel[i+36] = new Color(1f, 1f/6f*(5-i), 0f);
		}
		for (int i = 0; i < 6; i++) {
			colorLabel[i+42] = black;
		}
	}

	// Update is called once per frame
	void Update () {

		for (int i = 0; i < 6; i++) {
			frame[i] += Time.deltaTime;
			if (frame [i] > currProp [i].time) {
				obj [i].renderer.material.color = currProp [i].color;
				currProp [i] = props [i].getProp ();
				frame [i] = 0;
			}
		}
	}	
}

public class ChangePropaty{
	private int scene = 0;
	private int no = 0;
	private int maxNo = 0;
	public List<Propaty> prop = new List<Propaty>();
	
	public void add(float t, Color c){
		prop.Add(new Propaty(t, c));
		maxNo++;
	}
	
	public Propaty getProp(){
		Propaty p;
		p = prop [no++];
		if (no >= maxNo) {
			no = 0;
			// SceneManager.Instance.LoadLevel("ProjectionMappingForCube");
			// SceneManager.Instance.LoadLevel("Finish");
			SceneManager.Instance.NextScene();
		}
		return p;
	}
}

public class Propaty{
	public float time;
	public Color color;
	public Propaty(float t, Color c){
		time = t;
		color = c;
	}
}