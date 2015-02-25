/*
 The MIT License (MIT)

Copyright (c) 2013 yamamura tatsuhiko

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using UnityEngine;
using System.Collections;

public class AutoScaleQuad : MonoBehaviour {

	public Transform targetQuad;

	public ScaleType scaleType = ScaleType.Box;
	public bool scalableMask = false;


	public enum ScaleType
	{
		Fit,
		Box
	}

	void Update ()
	{
		UpdateScale ();
	}
	
	[ContextMenu("execute")]
	void UpdateScale ()
	{

		float height = camera.orthographicSize * 2;
		float width = height * camera.aspect;

		if( scaleType == ScaleType.Box )
		{
			float higherScale =  Mathf.Max(width, height);
			targetQuad.transform.localScale = new Vector3 (higherScale, higherScale, 0);
		}else 
		{
			targetQuad.transform.localScale = new Vector3 (width, height, 0);
		}

		targetQuad.transform.localPosition = Vector3.zero + transform.forward;

		if( scalableMask )
		{
			float rate = height / width;
			targetQuad.renderer.material.SetTextureScale("_MaskTex", new Vector2(1, rate) );
			targetQuad.renderer.material.SetTextureOffset("_MaskTex", new Vector2(0, (1 - rate) / 2 ) );
		}

		enabled = false;
	}
}
