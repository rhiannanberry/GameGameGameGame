using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using System.Drawing;
using System.Drawing.Imaging;

public class GameToGif : MonoBehaviour
{
	#if UNITY_EDITOR

	//konstructor
    private static GameToGif Instance { get; set; }

    [Header ("Recording Settings")]
    public string fileName = "screenshot";

	public string screenshotKey = "z";
	public string gifToggleKey = "x";

	public bool autoStartGIFCapture = false;

	[Range(10, 100)]
	public int millisecondsPerFrame = 10;

	[Range(0, 20)] 
	public int maxCaptureSeconds = 10;

	public RectTransform cursor;

    private Texture2D _texture;
	private bool _captureActive = false;

    //do not destroy object when changing or reloading scenes
	void Awake(){
		if (Instance){
			Destroy (gameObject);
		} else {
			Instance = this;
			DontDestroyOnLoad (gameObject);
			if (autoStartGIFCapture) StartCoroutine(GifCapture());
				
		}
	}

    //listen for keyboard button press
	void Update(){
		CheckInput();
		UpdateCursor();
	}

	private void UpdateCursor() {
		Vector2 vec;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(cursor.transform.parent.GetComponent<RectTransform>(), Input.mousePosition, null, out vec);
		cursor.anchoredPosition = vec;
		
	}

	IEnumerator GifCapture() {
		List<Bitmap> _bmps = new List<Bitmap>();
		float duration = 0f;

		_captureActive = true;

		while((duration < maxCaptureSeconds) && _captureActive) {
			duration += Time.deltaTime;

			//wait for graphics to render
			yield return new WaitForEndOfFrame();

			// create a _texture to pass to encoding
			_texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

			//put buffer into _texture
			_texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
			_texture.Apply();

			//split the process up--ReadPixels() and the GetPixels() call inside of the encoder are both pretty heavy
			yield return 0;

			byte[] bytes = _texture.EncodeToPNG();

			_bmps.Add(new Bitmap(new MemoryStream(bytes)));

			yield return new WaitForSeconds(1f / millisecondsPerFrame);
		}
		_captureActive = false;

		if (duration >= maxCaptureSeconds) {
			CaptureEndMaxText();
		} else {
			CaptureEndNormalText();
		}

		string timestamp = System.DateTime.Now.ToString ("dd_MM_yyyy_HH_mm_ss");
		string filePath = Application.dataPath + "/../Assets/GameToGif/ScreenCapture/"+ fileName + "_" + timestamp;
		
		ConvertToGif.Convert(filePath, millisecondsPerFrame, _bmps);

		UnityEditor.AssetDatabase.Refresh();
	}

    //capture screen
    IEnumerator ImageCapture() {
		//wait for graphics to render
		yield return new WaitForEndOfFrame();

		// create a _texture to pass to encoding
		_texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

		//put buffer into _texture
		_texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		_texture.Apply();

		//split the process up--ReadPixels() and the GetPixels() call inside of the encoder are both pretty heavy
		yield return 0;

		byte[] bytes = _texture.EncodeToPNG();

		//sufix for filename
		string timestamp = System.DateTime.Now.ToString ("dd_MM_yyyy_HH_mm_ss");

		//save image
		File.WriteAllBytes(Application.dataPath + "/../Assets/GameToGif/ScreenCapture/"+ fileName +"_" + timestamp + ".png", bytes);

		UnityEditor.AssetDatabase.Refresh();

	}

	private void CheckInput() {
		if (Input.GetKeyDown(screenshotKey))
			StartCoroutine(ImageCapture());
		
		if (Input.GetKeyDown(gifToggleKey)) {
			_captureActive = !_captureActive;
			if (_captureActive) StartCoroutine(GifCapture());
		}
	}

	private void CaptureStartText() {
		Debug.Log("<color=green> <b> Game To Gif Capture Started </b> </color>");
	}

	private void CaptureEndNormalText() {
		Debug.Log("<color=green> <b> Game To Gif Capture Ended </b> </color>");
	}

	private void CaptureEndMaxText() {
		Debug.Log("<color=orange> <b>Max Length Reached </b> </color>");
	}

	#endif
}