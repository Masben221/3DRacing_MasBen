using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAnimationByDelay : MonoBehaviour 
{

	[SerializeField]
	private float Delay = 0;

	void Awake()	 
	{
		GetComponent<Animator>().enabled = false;
	}

	void Start () 
	{
		Timer_old.CreateTimer(Delay, delegate() {
			GetComponent<Animator>().enabled = true;
		});
	}
	

}
