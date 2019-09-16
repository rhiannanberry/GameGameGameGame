using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject _ExitButton = null;
    // Start is called before the first frame update
    void Start()
    {
        #if !UNITY_STANDALONE
            _ExitButton.SetActive(false);
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitGame() {
        #if UNITY_EDITOR
         // Application.Quit() does not work in the editor so
         // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
         UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
