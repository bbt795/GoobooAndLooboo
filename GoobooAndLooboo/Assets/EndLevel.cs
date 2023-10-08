using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{

    public string nextGameScene;

    public GameObject Gooboo;
    public GameObject Looboo;

    public float goobooDistance;
    public float loobooDistance;

    // Start is called before the first frame update
    void Start()
    {

        Gooboo = GameObject.Find("Gooboo");
        Looboo = GameObject.Find("Looboo");
        
    }

    // Update is called once per frame
    void Update()
    {
        goobooDistance = Vector2.Distance(Gooboo.transform.position, this.gameObject.transform.position);
        loobooDistance = Vector2.Distance(Looboo.transform.position, this.gameObject.transform.position);

        Debug.Log(goobooDistance + " and " + loobooDistance);

        if (goobooDistance < 1f && loobooDistance < 1f)
        {

            SceneManager.LoadScene(nextGameScene);

        }

    }
}
