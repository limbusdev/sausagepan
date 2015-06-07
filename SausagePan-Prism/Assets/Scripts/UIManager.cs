﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
	
	public Image sound;

	public void Mute()
	{
		AudioListener.volume = 0;
		sound.enabled = false;
	}

	public void Sound()
	{
		AudioListener.volume = 1;
		sound.enabled = true;
	}

	public void QuitGame() {
		Application.Quit ();
		Application.ExternalEval ("window.close();");
	}

	public void RestartLevel(int levelNr) {
		Application.LoadLevel (levelNr);
	}
}