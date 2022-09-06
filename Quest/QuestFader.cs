using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestFader : MonoBehaviour {

	public Animator anim; 
	bool isFading = false;
	public float waitTime;
	[SerializeField] private Text questText;

	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
	}

	public void FadeIn(float time, string text){

		if (!isFading) {
			questText.text = text;
			isFading = true;
			anim.SetTrigger ("FadeIn");

			Invoke ("FadeOut", time);
		}
	}

	public void FadeOut()
	{
		isFading = true;
		anim.SetTrigger ("FadeOut");
		AnimationComplete ();
	}

	void AnimationComplete()
	{
		isFading = false;
	}
}
