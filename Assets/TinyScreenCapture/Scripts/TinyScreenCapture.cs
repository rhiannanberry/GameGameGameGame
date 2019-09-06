using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class TinyScreenCapture : MonoBehaviour
{
	#if UNITY_EDITOR

	//konstructor
    private static TinyScreenCapture Instance { get; set; }

    [Header ("Prefix for saved filed.")]
    public string fileName = "screenshoot";

	[Range(0.1f, 1f)]
	public float captureFrequency = .1f;

	public RectTransform cursor;

    private Texture2D texture;
	private bool _captureActive = false;
	private int _frameCount = 0;
	private float _time = 0f;

    //do not destroy object when changing or reloading scenes
	void Awake(){
		if (Instance){
			Destroy (gameObject);
		} else {
			Instance = this;
			DontDestroyOnLoad (gameObject);
		}
	}

    //listen for keyboard button press
	void Update(){
        //you can change selected keyboard button for different favorite button
		if (Input.GetKeyDown("s"))
			StartCoroutine(TinyCapture());

		if (Input.GetKeyDown("v")) {
			_captureActive = !_captureActive;
			_frameCount = 0;
			_time = captureFrequency;
		}

		if (_captureActive) {
			_time += Time.deltaTime;
			if (_time >= captureFrequency) {
				_time = 0f;
				StartCoroutine(TinyCaptureClip());
			}
		}

		UpdateCursor();
	}

	private void UpdateCursor() {
		Vector2 vec;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(cursor.transform.parent.GetComponent<RectTransform>(), Input.mousePosition, null, out vec);
		cursor.anchoredPosition = vec;
		
	}

    //capture screen
    IEnumerator TinyCapture() {
		//wait for graphics to render
		yield return new WaitForEndOfFrame();

		// create a texture to pass to encoding
		texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

		//put buffer into texture
		texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		texture.Apply();

		//split the process up--ReadPixels() and the GetPixels() call inside of the encoder are both pretty heavy
		yield return 0;

		byte[] bytes = texture.EncodeToPNG();
        //sufix for filename
		string timestamp = System.DateTime.Now.ToString ("dd_MM_yyyy_HH_mm_ss");

		//save image
        File.WriteAllBytes(Application.dataPath + "/../Assets/TinyScreenCapture/ScreenCapture/"+ fileName +"_" + timestamp + ".png", bytes);
	}

	//capture screen
    IEnumerator TinyCaptureClip() {
		//wait for graphics to render
		yield return new WaitForEndOfFrame();

		// create a texture to pass to encoding
		texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

		//put buffer into texture
		texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		texture.Apply();

		//split the process up--ReadPixels() and the GetPixels() call inside of the encoder are both pretty heavy
		yield return 0;

		byte[] bytes = texture.EncodeToPNG();

		string directoryPath = Application.dataPath + "/../Assets/TinyScreenCapture/ScreenCapture/"+ fileName;

		if(!Directory.Exists(directoryPath)) {    
			//if it doesn't, create it
			Directory.CreateDirectory(directoryPath);
		
		}

		//save image
        File.WriteAllBytes(directoryPath + "/" + (_frameCount++) + ".png", bytes);
	}

	#endif
}