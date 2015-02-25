// Copyright (c) 2014 subakolab.inc
// 
// NYSL Version 0.9982
// A. This software is "Everyone's Ware". It means:
//   Anybody who has this software can use it as if he/she is
//   the author.
// 
//   A-1. Freeware. No fee is required.
//   A-2. You can freely redistribute this software.
//   A-3. You can freely modify this software. And the source
//       may be used in any software with no limitation.
//   A-4. When you release a modified version to public, you
//       must publish it with your name.
// 
// B. The author is not responsible for any kind of damages or loss
//   while using or misusing this software, which is distributed
//   "AS IS". No warranty of any kind is expressed or implied.
//   You use AT YOUR OWN RISK.
// 
// C. Copyrighted to subakolab.inc(http://www.subakolab.com/)
// 
// D. Above three clauses are applied both to source and binary
//   form of this software.

using System;
using System.Collections;
using UnityEngine;

public delegate void iTweenAction(GameObject target, Hashtable args);

public class iTweenExtention : MonoBehaviour
{
	/// <summary>
	/// play iTween animations to seriall
	/// </summary>
	/// <param name="target">target of the animation</param>
	/// <param name="param">Parameter of the animation</param>
	public static void SerialPlay(GameObject target, params object[] param){
		
		//initialize
		var iTweenEx = target.GetComponent<iTweenExtention>();
		if (!iTweenEx) {
			iTweenEx = target.AddComponent<iTweenExtention>();
		}
		
		var firstAction = param[0] as iTweenAction;
		var firstArg = param[1] as Hashtable;
		
		//create iTween chain
		var preArg = firstArg;
		for (int index = 2; index < param.Length; index+=2) {
			var action = param[index] as iTweenAction;
			var args = param[index+1] as Hashtable;
			
			Action callback = ()=>action(target, args);
			
			preArg.Add("oncomplete", "HandleTweenComplete");
			preArg.Add("oncompleteparams", callback);
			preArg.Add("oncompletetarget", target);
			
			iTweenEx.waitCount++;
			
			preArg = args;
		}
		
		//call first
		firstAction(target, firstArg);
	}
	
	protected int waitCount = 0;
	
	protected void HandleTweenComplete(Action callback)
	{
		//run callback
		if (callback != null) {
			callback();
		}
		
		//clean
		this.waitCount--;
		if (this.waitCount == 0) {
			Destroy(this);	
		}
	}
}