using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CinematicBars : MonoBehaviour
{
	private RectTransform topBar, bottomBar, leftBar;

	private void Awake() {
		GameObject top = new GameObject("topBar", typeof(Image));
		top.transform.SetParent(transform, false);
		top.GetComponent<Image>().color = Color.black;
		topBar = top.GetComponent<RectTransform>();
		topBar.anchorMin = new Vector2(0 ,1);
		topBar.anchorMax = new Vector2(1, 1);
		topBar.sizeDelta = new Vector2(0 , 175);

		GameObject bottom = new GameObject("bottomBar", typeof(Image));
		bottom.transform.SetParent(transform, false);
		bottom.GetComponent<Image>().color = Color.black;
		bottomBar = bottom.GetComponent<RectTransform>();
		bottomBar.anchorMin = new Vector2(0 ,0);
		bottomBar.anchorMax = new Vector2(1, 0);
		bottomBar.sizeDelta = new Vector2(0 , 175);
	}		
}
