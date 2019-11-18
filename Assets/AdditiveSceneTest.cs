using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveSceneTest : MonoBehaviour
{
    [Scene] public string game;
    void Awake() {
        Camera.main.gameObject.SetActive(false);
        SceneManager.LoadScene(game, LoadSceneMode.Additive);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
