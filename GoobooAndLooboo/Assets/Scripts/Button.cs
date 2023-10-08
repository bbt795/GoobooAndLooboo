using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject myTarget;
    public SpriteRenderer myRenderer;
    public Sprite upSprite;
    public Sprite pressedSprite;
    public Rigidbody2D myRig;
    // Start is called before the first frame update
    void Start()
    {
        myRenderer = this.GetComponent<SpriteRenderer>();
        myRig = this.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter(Collider other){
            myRenderer.sprite = pressedSprite;
            myTarget = GameObject.FindGameObjectWithTag("LoobooPlatform");
            myTarget.SetActive(true);
        
    }

    private void OnTriggerExit(Collider other){
            myRenderer.sprite = upSprite;
            myTarget = GameObject.FindGameObjectWithTag("LoobooPlatform");
            myTarget.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
