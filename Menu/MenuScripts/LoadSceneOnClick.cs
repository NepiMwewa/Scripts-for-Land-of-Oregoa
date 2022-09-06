using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
	[SerializeField] private string loadScreen;
	private XMLManager xmlManager;

	public void LoadByString(string sceneToLoad)
    {
		SceneManager.LoadScene(sceneToLoad);
    }

	public void LoadSavedGame(bool value)
	{
		xmlManager = FindObjectOfType<XMLManager> ();
		xmlManager.LoadGame ();

		if (GameInformation.saveFound) {

			GameInformation.loadSave = value;
			SceneManager.LoadScene (loadScreen);
		} else {
			//bring up a popup window that tells the user there is no save found
			//Debug.Log("no save found");
		}
	}
}