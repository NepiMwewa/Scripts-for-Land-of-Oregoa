using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewArea : MonoBehaviour {

	public string levelToLoad;


	public string exitPoint;

	private PlayerController thePlayer;
	private GameController gController;
	// Use this for initialization
	void Start () {
		thePlayer = FindObjectOfType<PlayerController> ();
		gController = FindObjectOfType<GameController> ();
	} 
	
	// Update is called once per frame
	void Update () {
		 
	}

	IEnumerator OnTriggerEnter2D(Collider2D other)   
	{
		if (other.gameObject.name == "Player") {

		//	fade.BeginFade (1);
			gController.CloseWindows();

			ScreenFader sf = GameObject.FindGameObjectWithTag ("Fader").GetComponent<ScreenFader> ();

			yield return StartCoroutine(sf.FadeToBlack());

			ChangeLevel ();

			yield return StartCoroutine(sf.FadeToClear());
		}
	}

	public void ChangeLevel()
	{
		//GameInformation.GameStats.MapSlug = levelToLoad;
		SceneManager.LoadScene (levelToLoad);
		thePlayer.startPoint = exitPoint;
	}
}
