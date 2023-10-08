using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutsceneEnd : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    public string newGameScene;

    // Start is called before the first frame update
    void Start()
    {

        videoPlayer.loopPointReached += NextScene;

    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void NextScene(VideoPlayer videoPlayer){

            SceneManager.LoadScene(newGameScene);

    }
}
