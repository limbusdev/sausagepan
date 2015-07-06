﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UIBottomManager : MonoBehaviour {


	// Create lists of GameObjects for the color circles
	public List<GameObject> colorCircleXL = new List<GameObject>();		
	public List<GameObject> colorCircleXS = new List<GameObject>();
	
	// List of all colors
	public List<Color> fullColorList = new List<Color> ();

	// Black and white cloud
	public GameObject blackCloud;
	public GameObject whiteCloud;

	// Help menu
	public GameObject helpMenu;

	GameObject player;

	// Set bool variables for the back and white cloud
	bool blackCloudIsActive = false;
	bool whiteCloudIsActive = false;
	
	bool lightColorCircleXL_isActive = true;

	
	/**
	 * Switch color circles to right
	 * */
	public void SwitchToRight()
	{
		LightColorCircleXL();					// Call function that set additive colors in the big color circles
		ColorColorCircleXS ();					// Call function that set subtractive colors in the small color circles

		lightColorCircleXL_isActive = true;		// Set the big additive color circles true 
	}

	/**
	 * Switch color circles to right
	 * */
	public void SwitchToLeft()
	{
		ColorColorCircleXL ();					// Call function that set subtractive colors in the big color circles
		LightColorCircleXS ();					// Call function that set additive colors in the small color circles

		lightColorCircleXL_isActive = false;
	}

	/**
	 * Open help menu
	 **/
	public void HelpMenu()
	{
		helpMenu.SetActive (true);				// Activate the helpMenu GameObject 
	}

	/**
	 * Activate the GameObject of black cloud and set it position on top of player
	 **/
	public void CallBlackCloud()
	{
		if (!blackCloudIsActive) 
		{
			if(whiteCloudIsActive)
			{
				ResetWhiteCloud();				// Call function that will hide the white cloud

				blackCloudIsActive = true;		// Set the blackCloudIsActive (bool) variable true
				
				blackCloud.SetActive (true);	// Activate the GameObject of black cloud
				blackCloud.GetComponent<Transform> ().localPosition = new Vector3 (player.GetComponent<Transform> ().localPosition.x,
				                                                                   player.GetComponent<Transform> ().localPosition.y + 5,
				                                                                   player.GetComponent<Transform> ().localPosition.z);

				Invoke ("ResetBlackCloud", 5);	// Call, after a delay of 5 seconds, a function that will hide the black cloud	
			}
			else
			{
				blackCloudIsActive = true;		// Set the blackCloudIsActive (bool) variable true
				
				blackCloud.SetActive (true);	// Activate the GameObject of black cloud	
				blackCloud.GetComponent<Transform> ().localPosition = new Vector3 (player.GetComponent<Transform> ().localPosition.x,
				                                                                   player.GetComponent<Transform> ().localPosition.y + 5,
				                                                                   player.GetComponent<Transform> ().localPosition.z);

				Invoke ("ResetBlackCloud", 5);	// Call, after a delay of 5 seconds, a function that will hide the black cloud
			}
		}
	}

	/**
	 * Activate the GameObject of whtie cloud and set it position on top of player
	 **/
	public void CallWhiteCloud()
	{
		if (!whiteCloudIsActive) 
		{
			if(blackCloudIsActive)
			{
				ResetBlackCloud();				// Call function that will hide the black cloud

				whiteCloudIsActive = true;		// Set the whiteCloudIsActive (bool) variable true
				
				whiteCloud.SetActive (true);	// Activate the GameObject of white cloud
				whiteCloud.GetComponent<Transform> ().localPosition = new Vector3 (player.GetComponent<Transform> ().localPosition.x,
				                                                                   player.GetComponent<Transform> ().localPosition.y + 5,
				                                                                   player.GetComponent<Transform> ().localPosition.z);

				Invoke ("ResetWhiteCloud", 5);	// Call, after a delay of 5 seconds, a function that will hide the white cloud
			}
			else
			{
				whiteCloudIsActive = true;		// Set the whiteCloudIsActive (bool) variable true
				
				whiteCloud.SetActive (true);	// Activate the GameObject of white cloud
				whiteCloud.GetComponent<Transform> ().localPosition = new Vector3 (player.GetComponent<Transform> ().localPosition.x,
				                                                                   player.GetComponent<Transform> ().localPosition.y + 5,
				                                                                   player.GetComponent<Transform> ().localPosition.z);

				Invoke ("ResetWhiteCloud", 5); 	// Call, after a delay of 5 seconds, a function that will hide the white cloud
			}
		}
	}


	/**
	 * Initialize all colors in game into a list
	 **/
	public void InitializeFullColorList(List<Color> colorList)
	{
		// Clean old list
		fullColorList.Clear ();

		fullColorList.Add (new Color (1, 1, 1, 1)); // 0 white
		fullColorList.Add (new Color (1, 1, 0, 1)); // 1 yellow
		fullColorList.Add (new Color (1, 0, 1, 1)); // 2 magenta
		fullColorList.Add (new Color (1, 0, 0, 1)); // 3 red
		fullColorList.Add (new Color (0, 1, 1, 1)); // 4 cyan
		fullColorList.Add (new Color (0, 1, 0, 1)); // 5 green
		fullColorList.Add (new Color (0, 0, 1, 1)); // 6 blue
		fullColorList.Add (new Color (0, 0, 0, 1)); // 7 black
		
		FillColorCircle (colorList);
	}

	/**
	 * Override each element int the old fullColorList with grey,
	 * if colors aren't allready found in game
	 **/
	public void FillColorCircle(List<Color> colorList)
	{
		int i = 0;
		int j = 0;

		// List with IDs of full color list elements
		List<int> tempColorIDList = new List<int> {0, 1, 2, 3, 4, 5, 6, 7};

		// Search in full color list all found colors in game
		// Remove all found indexes in tempColorIDList
		foreach(Color cl in colorList.ToList())
		{
			foreach(Color fcl in fullColorList.ToList())
			{
				if(cl.Equals(fcl))
				{
					foreach(int tcIDl in tempColorIDList.ToList())
					{
						tempColorIDList.Remove(i);
					}

					//Debug.Log ("tempColorIDList " + tempColorIDList.Count);
				}

				i++;
			}

			i = 0;
		}

		tempColorIDList.Sort ();				// Sort tempColorIDList

		// Override each element int the old fullColorList with grey,
		// that aren't allready found in game
		foreach(Color fcl in fullColorList.ToList())
		{
			foreach(int tcIDl in tempColorIDList)
			{
				if(j == tcIDl)
				{
					fullColorList[j] = colorList[0];
				}
			}

			j++;
		}

		//Debug.Log ("tempColorIDList " + tempColorIDList.Count);

		// Set old color circles combination 
		if (lightColorCircleXL_isActive)
			SwitchToRight ();
		else
			SwitchToLeft ();
	}

	/**
	 * Color Circles
	 * 
	 * 0 = bottom_left
	 * 1 = bottom_right
	 * 2 = top_left
	 * 3 = top_right
	 * 4 = top_mid
	 * 5 = mid
	 * 6 = bottom
	 * */

	/**
	 * Set additive colors into big color circles
	 * */
	void LightColorCircleXL()
	{
		colorCircleXL [0].GetComponent<Image> ().color = fullColorList[4]; // cyan
		colorCircleXL [1].GetComponent<Image> ().color = fullColorList[1]; // yellow
		colorCircleXL [2].GetComponent<Image> ().color = fullColorList[6]; // blue
		colorCircleXL [3].GetComponent<Image> ().color = fullColorList[3]; // red 
		colorCircleXL [4].GetComponent<Image> ().color = fullColorList[2]; // magenta
		colorCircleXL [5].GetComponent<Image> ().color = fullColorList[0]; // white 
		colorCircleXL [6].GetComponent<Image> ().color = fullColorList[5]; // green
	}

	/**
	 * Set subtractive colors into big color circles
	 * */
	void ColorColorCircleXL()
	{
		colorCircleXL [0].GetComponent<Image> ().color = fullColorList[6]; // blue
		colorCircleXL [1].GetComponent<Image> ().color = fullColorList[3]; // red 
		colorCircleXL [2].GetComponent<Image> ().color = fullColorList[4]; // cyan 
		colorCircleXL [3].GetComponent<Image> ().color = fullColorList[1]; // yellow 
		colorCircleXL [4].GetComponent<Image> ().color = fullColorList[5]; // green
		colorCircleXL [5].GetComponent<Image> ().color = fullColorList[7]; // black 
		colorCircleXL [6].GetComponent<Image> ().color = fullColorList[2]; // magenta 
	}

	/**
	 * Set additive colors into small color circles
	 * */
	void LightColorCircleXS()
	{
		colorCircleXS [0].GetComponent<Image> ().color = fullColorList[4]; // cyan
		colorCircleXS [1].GetComponent<Image> ().color = fullColorList[1]; // yellow
		colorCircleXS [2].GetComponent<Image> ().color = fullColorList[6]; // blue
		colorCircleXS [3].GetComponent<Image> ().color = fullColorList[3]; // red 
		colorCircleXS [4].GetComponent<Image> ().color = fullColorList[2]; // magenta
		colorCircleXS [5].GetComponent<Image> ().color = fullColorList[0]; // white 
		colorCircleXS [6].GetComponent<Image> ().color = fullColorList[5]; // green
	}

	/**
	 * Set subtractive colors into small color circles
	 * */
	void ColorColorCircleXS()
	{
		colorCircleXS [0].GetComponent<Image> ().color = fullColorList[6]; // blue
		colorCircleXS [1].GetComponent<Image> ().color = fullColorList[3]; // red 
		colorCircleXS [2].GetComponent<Image> ().color = fullColorList[4]; // cyan 
		colorCircleXS [3].GetComponent<Image> ().color = fullColorList[1]; // yellow 
		colorCircleXS [4].GetComponent<Image> ().color = fullColorList[5]; // green
		colorCircleXS [5].GetComponent<Image> ().color = fullColorList[7]; // black 
		colorCircleXS [6].GetComponent<Image> ().color = fullColorList[2]; // magenta 
	}

	/**
	 * Reset black cloud
	 **/
	void ResetBlackCloud()
	{
		blackCloud.SetActive (false);
		blackCloudIsActive = false;
	}

	/**
	 * Reset white cloud
	 **/
	void ResetWhiteCloud()
	{
		whiteCloud.SetActive (false);
		whiteCloudIsActive = false;
	}

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}
}