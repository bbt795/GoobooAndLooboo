using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoNotDestroy : MonoBehaviour
{
    public string sceneTitle;
    public AudioSource source;

    private void Awake()
    {
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("GameAudio");
 
        if(musicObj.Length > 1)
        {

            Destroy(this.gameObject);

        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {

        source = this.gameObject.GetComponent<AudioSource>();

        if (SceneManager.GetActiveScene().name == sceneTitle)
        {

            source.Pause();

        }

    }
}
