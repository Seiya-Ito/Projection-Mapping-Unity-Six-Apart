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
using System;

[RequireComponent(typeof(Camera))]
public class FadeCamera : SingletonMonoBehaviour<FadeCamera> {

	public MeshRenderer targetRender;

	private float goalTime;
	private float time;
	private Material material;
	private bool fadein = true;
	private Action action;

	private static readonly string cutoff = "_Cutoff";
	private static readonly string mainTex = "_MainTex";
	private static readonly string maskTex = "_MaskTex";

	new protected  void Awake()
	{
		if( this.CheckInstance() )
		{
			DontDestroyOnLoad(gameObject);
			material = targetRender.material;
		}else{
			Destroy (gameObject);
		}
	}


	void OnEnable()
	{
		camera.enabled = true;
	}

	void OnDisable()
	{
		if(! fadein  )
			camera.enabled = false;
	}

	void Update () {

		if( goalTime < Time.time)
		{
			targetRender.gameObject.SetActive(fadein);
			enabled = false;
			if( action != null)
				action();
		}

		float diff = goalTime - Time.time;
		if( fadein )
		{
			float rate = (diff / time) * 2;
			material.SetFloat(cutoff, rate - 1);
		}else{
			float rate = 2- (diff / time) * 2;
			material.SetFloat(cutoff, rate - 1);
		}
	}

	public bool fading
	{
		get{
			return goalTime > Time.time;
		}
	}

	public void UpdateTexture(Texture texture)
	{
		material.SetTexture(mainTex, texture);
	}

	public void UpdateMaskTexture(Texture texture)
	{
		material.SetTexture(maskTex, texture);
	}

	public void FadeOut(float requestTime, Action act)
	{
		fadein = true;
		TimerSetup(requestTime, act);
	}

	public void FadeIn(float requestTime, Action act)
	{
		fadein = false;
		TimerSetup(requestTime, act);
	}

	void TimerSetup(float requestTime, Action act)
	{
		targetRender.gameObject.SetActive(true);
		action = act;
		time = requestTime;
		goalTime = Time.time + time;
		enabled = true;
	}
}

