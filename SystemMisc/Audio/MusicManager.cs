using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {


	private List<AudioSource> musicTracks = null;//split this up so that there is different tracks. ie spooky, rpg overworld, etc

	[SerializeField] private AudioSource trackA, trackB;

	[SerializeField] private List<AudioSource> mainMenu;
	[SerializeField] private List<AudioSource> spaceRegion1, spaceRegion2, spaceRegion3;
	[SerializeField] private List<AudioSource> landRegion1, landRegion2, landRegion3;
	[SerializeField] private List<AudioSource> tavern1, tavern2, tavern3;
	[SerializeField] private List<AudioSource> battle1, battle2, battle3;
	[SerializeField] private List<AudioSource> HighClassInn;
	[SerializeField] private List<AudioSource> AllMusic;

	public BaseMusicTrack.MusicRegion currentTrackPlaylist;

	private int currentTrackCounter = 0;
	private int tempInt;

	private float StartFadeTime;
	[SerializeField] private float timeToFadeSwitch;
	[SerializeField] private float timeToFadeTrack;

	private bool switchTrack = false;
	private bool fading = false;
	private bool waitToFade = false;
	private bool nextTrackBool = false;

	private BaseMusicTrack.MusicRegion waitToFadeRegion;

	public bool musicCanPlay;

	private VolumeManager vManager;


	// Use this for initialization
	void Awake () {
		vManager = FindObjectOfType<VolumeManager> ();

		//battle1 [0].Play ();

		/*musicTracks = mainMenu;

		trackA = musicTracks [0];

		trackA.volume = 0.5f;
		StartFadeTime = trackA.clip.length - timeToFade;

		trackA.Play ();*/

		//if (CurrentTracks.Length != 0) {
		//	PlayMusic ();
		//}
	}

	private IEnumerator FadeBetweenTracks(AudioSource currentTrack, AudioSource newTrack, float seconds)
	{
		float stepIntervalAmount = seconds / 20.0f;
		float volStepAmountNew = newTrack.volume / 20.0f;// change this to bvolume and after it stops reset the b.volume to og volume\
		float volStepAmountCurrent = currentTrack.volume / 20.0f;
		fading = true;
		newTrack.volume = 0.0f;
		newTrack.Play ();
		//Debug.Log ("called");
		for(int i = 0; i < 20; i++)
		{
			currentTrack.volume -= volStepAmountCurrent;
			newTrack.volume += volStepAmountNew;
			yield return new WaitForSecondsRealtime(stepIntervalAmount);
		}
		currentTrack.volume = 0f;
		currentTrack.Stop ();
		fading = false;
	}

	private IEnumerator FadeTrack(AudioSource temp, float fadeTime)
	{
		bool play_a = true;
		if(trackA.isPlaying){ 
			play_a = false;
		}
		if (play_a) {
			trackA.clip = temp.clip;
			trackA.volume = temp.volume;
			StartFadeTime = trackA.clip.length - fadeTime;
			yield return StartCoroutine (FadeBetweenTracks (trackB, trackA, fadeTime));
		} else {
			trackB.clip = temp.clip;
			trackB.volume = temp.volume;
			StartFadeTime = trackB.clip.length - fadeTime;
			yield return StartCoroutine (FadeBetweenTracks (trackA, trackB, fadeTime));
		}
	}

	private void PlayNextTrack()
	{
		if (musicTracks.Count == 1) {
			currentTrackCounter = 0;
		} else {
			int i = currentTrackCounter;
			while (i == currentTrackCounter) {
				i = GetRandomNumber (musicTracks.Count);
			}
			currentTrackCounter = i;
		}

		StartCoroutine (FadeTrack (musicTracks[currentTrackCounter], timeToFadeTrack));
	}
	private void SwitchPlayList()
	{
		if (musicTracks.Count == 1) {
			currentTrackCounter = 0;
		} else {
			int i = currentTrackCounter;
			while (i == currentTrackCounter) {
				i = GetRandomNumber (musicTracks.Count);
			}
			currentTrackCounter = i;
		}

		StartCoroutine (FadeTrack (musicTracks[currentTrackCounter], timeToFadeSwitch));
	}
	
	// Update is called once per frame
	void  FixedUpdate () {// once track is done playing make it play the next track in the playlist it is currently on

		if (currentTrackPlaylist != BaseMusicTrack.MusicRegion.Null && musicCanPlay) {

			if (fading) {
				return;
			}
			if (waitToFade) {
			////Debug.Log ("wait to fade");
				waitToFade = false;
				if (nextTrackBool) {
					nextTrackBool = false;
					NextTrack ();
				} else {
					SwitchTrackRegion (waitToFadeRegion);
				}
				return;
			}

			if (switchTrack) {
				////Debug.Log ("switch track");
				switchTrack = false;
				currentTrackCounter = -1;
				SwitchPlayList ();
				return;
			}
			//	//Debug.Log ("a is playing " + trackA.isPlaying);
				if (trackA.isPlaying) {
					if (trackA.time >= StartFadeTime) {
			//			//Debug.Log ("called track a");
						PlayNextTrack ();
						//start fade
					}
				}
			//	//Debug.Log ("b is playing " + trackB.isPlaying);
				if (trackB.isPlaying) {
					if (trackB.time >= StartFadeTime) {
			//			//Debug.Log ("called track b");
						PlayNextTrack ();
						//trackB.Play ();
						//start fade
					}
				}
		}
		/*if (currentTrackPlaylist != BaseMusicTrack.MusicRegion.Null && musicCanPlay) {
			if (!musicTracks [currentTrackCounter].isPlaying && isPaused) {
				if (musicTracks.Count > 1) {
					tempInt = GetRandomNumber (musicTracks.Count);
					if (tempInt == currentTrackCounter) {
						if (0 == currentTrackCounter) {
							++currentTrackCounter;
						} else {
							--currentTrackCounter;
						}
					} else {
						currentTrackCounter = tempInt;
					}
				} else {
					currentTrackCounter = 0;
				}
				//Debug.Log("time " + musicTracks [currentTrackCounter].clip.length.ToString());
				musicTracks [currentTrackCounter].Play ();
				turnOffMusicInPlayerlist ();
			} else if (musicTracks [currentTrackCounter].isPlaying) {
				////Debug.Log ("time: " + musicTracks [currentTrackCounter].clip.length);
			}
		} else {
			turnOffAllMusic ();
		}*/
	}
		
	private void turnOffAllMusic()
	{
		for (int i = 0; i < AllMusic.Count; i++) {
			AllMusic [i].Stop ();
		}
	}

	private void turnOffMusicInPlayerlist (){
		for (int i = 0; i < musicTracks.Count; i++) {
			if (i != currentTrackCounter) {
				musicTracks [i].Stop ();
			}
		}
	}

	private AudioSource newTrack()
	{
		if (musicTracks.Count > 1) {
			tempInt = GetRandomNumber (musicTracks.Count);
			if (tempInt == currentTrackCounter) {
				if (0 == currentTrackCounter) {
					++currentTrackCounter;
				} else {
					--currentTrackCounter;
				}
			} else {
				currentTrackCounter = tempInt;
			}
		} else {
			currentTrackCounter = 0;
		}
		//Debug.Log ("time " + musicTracks [currentTrackCounter].clip.length.ToString ());
		StartFadeTime = musicTracks [currentTrackCounter].clip.length - timeToFadeTrack;
		fading = true;
		musicTracks [currentTrackCounter].volume = 0.0f;
		return musicTracks [currentTrackCounter];
	}

	public void NextTrack()
	{
		if (fading) {// if fading then wait until its done fading
			waitToFadeRegion = currentTrackPlaylist;
			waitToFade = true;
			return;
		} else {
			PlayNextTrack ();
		}
	}

	public void SwitchTrackRegion(BaseMusicTrack.MusicRegion tempRegion){

		if (fading) {// if fading then wait until its done fading
			waitToFadeRegion = tempRegion;
			waitToFade = true;
			return;
		}

		if(tempRegion == BaseMusicTrack.MusicRegion.Null){
			currentTrackPlaylist = tempRegion;
			turnOffAllMusic ();
			return;
		}else if (currentTrackPlaylist == tempRegion) {
			//Debug.Log (" same track");
			return;
		}
		//Debug.Log ("not same track");
		currentTrackPlaylist = tempRegion;

		switch (currentTrackPlaylist) {
		case BaseMusicTrack.MusicRegion.MainMenu:
			{
				musicTracks = mainMenu;
				break;
			}
		case BaseMusicTrack.MusicRegion.SpaceRegion1:
			{
				musicTracks = spaceRegion1;
				break;
			}
		case BaseMusicTrack.MusicRegion.SpaceRegion2:
			{
				musicTracks = spaceRegion2;
				break;
			}
		case BaseMusicTrack.MusicRegion.SpaceRegion3:
			{
				musicTracks = spaceRegion3;
				break;
			}
		case BaseMusicTrack.MusicRegion.LandRegion1:
			{
				musicTracks = landRegion1;
				break;
			}
		case BaseMusicTrack.MusicRegion.LandRegion2:
			{
				musicTracks = landRegion2;
				break;
			}
		case BaseMusicTrack.MusicRegion.LandRegion3:
			{
				musicTracks = landRegion3;
				break;
			}
		case BaseMusicTrack.MusicRegion.Tavern1:
			{
				musicTracks = tavern1;
				break;
			}
		case BaseMusicTrack.MusicRegion.Tavern2:
			{
				musicTracks = tavern2;
				break;
			}
		case BaseMusicTrack.MusicRegion.Tavern3:
			{
				musicTracks = tavern3;
				break;
			}
		case BaseMusicTrack.MusicRegion.Battle1:
			{
				musicTracks = battle1;
				break;
			}
		case BaseMusicTrack.MusicRegion.Battle2:
			{
				musicTracks = battle2;
				break;
			}
		case BaseMusicTrack.MusicRegion.Battle3:
			{
				musicTracks = battle3;
				break;
			}
		case BaseMusicTrack.MusicRegion.HighClassInn:
			{
				musicTracks = HighClassInn;
				break;
			}
		}
		switchTrack = true;

	}

	public void StartTrack(BaseMusicTrack.MusicRegion tempRegion){

		if(tempRegion == BaseMusicTrack.MusicRegion.Null){
			currentTrackPlaylist = tempRegion;
			turnOffAllMusic ();
			return;
		}else if (currentTrackPlaylist == tempRegion) {
			return;
		}

		turnOffAllMusic ();

		currentTrackPlaylist = tempRegion;

		switch (currentTrackPlaylist) {
		case BaseMusicTrack.MusicRegion.MainMenu:
			{
				musicTracks = mainMenu;
				break;
			}
		case BaseMusicTrack.MusicRegion.SpaceRegion1:
			{
				musicTracks = spaceRegion1;
				break;
			}
		case BaseMusicTrack.MusicRegion.SpaceRegion2:
			{
				musicTracks = spaceRegion2;
				break;
			}
		case BaseMusicTrack.MusicRegion.SpaceRegion3:
			{
				musicTracks = spaceRegion3;
				break;
			}
		case BaseMusicTrack.MusicRegion.LandRegion1:
			{
				musicTracks = landRegion1;
				break;
			}
		case BaseMusicTrack.MusicRegion.LandRegion2:
			{
				musicTracks = landRegion2;
				break;
			}
		case BaseMusicTrack.MusicRegion.LandRegion3:
			{
				musicTracks = landRegion3;
				break;
			}
		case BaseMusicTrack.MusicRegion.Tavern1:
			{
				musicTracks = tavern1;
				break;
			}
		case BaseMusicTrack.MusicRegion.Tavern2:
			{
				musicTracks = tavern2;
				break;
			}
		case BaseMusicTrack.MusicRegion.Tavern3:
			{
				musicTracks = tavern3;
				break;
			}
		case BaseMusicTrack.MusicRegion.HighClassInn:
			{
				musicTracks = HighClassInn;
				break;
			}
		}

		if (musicTracks.Count > 1) {
			tempInt = GetRandomNumber (musicTracks.Count);
			if (tempInt == currentTrackCounter) {
				if (0 == currentTrackCounter) {
					++currentTrackCounter;
				} else {
					--currentTrackCounter;
				}
			} else {
				currentTrackCounter = tempInt;
			}
		} else {
			currentTrackCounter = 0;
		}
		////Debug.Log("time " + musicTracks [currentTrackCounter].clip.length.ToString());

		StartFadeTime = musicTracks [currentTrackCounter].clip.length - 3f;
		trackA = musicTracks [currentTrackCounter];
		trackA.Play ();
	}



	private int GetRandomNumber(int maxInt)
	{
		return Random.Range (0, maxInt);
	}
}
