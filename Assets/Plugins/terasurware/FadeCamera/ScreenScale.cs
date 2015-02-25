using UnityEngine;
using System.Collections;

public class ScreenScale : MonoBehaviour {

	public int maxHeight = 1024;

	void Start () {

		float screenRate = (float)maxHeight / Screen.height;
		if( screenRate > 1 ) screenRate = 1;
		int width = (int)(Screen.width * screenRate);
		int height = (int)(Screen.height * screenRate);

		if( Screen.height != height )
			Screen.SetResolution( width , height, true, 15);
	}
}
