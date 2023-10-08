using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Looboo : MonoBehaviour
{
    public GameObject myTarget;
    public GameObject[] myTargets;
    public Animator myAnim;
    public SpriteRenderer myRenderer;
    public SpriteRenderer otherRenderer;
    public Rigidbody2D myRig;

    private BoxCollider2D coll;
    [SerializeField] private LayerMask jumpableGround;

    public Sprite upSprite;
    public Sprite pressedSprite;
    public bool solidPressed = false;

    public bool canJump = true;
    public bool lastJump = false;

    public int doubleJump = 0;

    public float speed = 5.0f;
    public Vector2 lastDirection;

    public void onMove(InputAction.CallbackContext ev)
    {
        //Debug.Log("Inside Callback");
        if (ev.performed)
        {
            lastDirection = ev.ReadValue<Vector2>();
        }
        if (ev.canceled)
        {
            lastDirection = Vector2.zero;
            myAnim.SetInteger("DIR", 0);
        }
    }

    public void onJump(InputAction.CallbackContext ev)
    {

        if (ev.started && canJump)
        {
            //started means you just pressed the button, prevents auto hop
            //if you want auto hop you can do ev.performed
            //canJump prevents repeatedly jumping player without stopping
            lastJump = true;
            myAnim.SetInteger("DIR", 2);
        }

    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Button" && !solidPressed){
            other.GetComponent<SpriteRenderer>().sprite = pressedSprite;
            myTarget = GameObject.FindGameObjectWithTag("LoobooPlatform");
            myTarget.GetComponent<Renderer>().enabled=true;
            myTarget.GetComponent<TilemapCollider2D>().enabled=true;
        }
        else if(other.tag == "ButtonSolid"){
            other.GetComponent<SpriteRenderer>().sprite = pressedSprite;
            myTarget = GameObject.FindGameObjectWithTag("GoobooPlatform");
            myTarget.GetComponent<Renderer>().enabled=true;
            myTarget.GetComponent<TilemapCollider2D>().enabled=true;
            myTarget = GameObject.FindGameObjectWithTag("LoobooPlatform");
            myTarget.GetComponent<Renderer>().enabled=true;
            myTarget.GetComponent<TilemapCollider2D>().enabled=true;
            GameObject.Find("Gooboo").GetComponent<Gooboo>().solidPressed = true;
            solidPressed = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.tag == "Button" && !solidPressed){
            other.GetComponent<SpriteRenderer>().sprite = upSprite;
            myTarget = GameObject.FindGameObjectWithTag("LoobooPlatform");
            myTarget.GetComponent<Renderer>().enabled=false;
            myTarget.GetComponent<TilemapCollider2D>().enabled=false;
        }
        else if(other.tag == "ButtonSolid"){
            other.GetComponent<SpriteRenderer>().sprite = upSprite;
        }
    }

    private bool IsGrounded(){
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    // Start is called before the first frame update
    void Start()
    {
        myAnim = this.GetComponent<Animator>();
        coll = this.GetComponent<BoxCollider2D>();
        myRenderer = this.GetComponent<SpriteRenderer>();
        myRig = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(myRig.velocity.y);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            if (myRenderer != null)
            {

                myRenderer.flipX = true;
                myAnim.SetInteger("DIR", 1);

            }

        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            if (myRenderer != null)
            {

                myRenderer.flipX = false;
                myAnim.SetInteger("DIR", 1);

            }
        }
        if(IsGrounded() && !canJump && myRig.velocity.x == 0){
            myAnim.SetInteger("DIR", 0);
        }
        if(myRig.velocity.y <= -0.2f){
            myAnim.SetInteger("DIR", 3);
        }

        //myRig.velocity = transform.forward * speed * lastDirection.y + new Vector3(0, myRig.velocity.y, 0);
        var velocity = new Vector2(lastDirection.x, 0f).normalized * speed;
        myRig.velocity = new Vector2(velocity.x, myRig.velocity.y);

        if (lastJump)
        {
            myRig.AddForce(new Vector2(0, 8), ForceMode2D.Impulse);
            //this is poor practice, it is better to have a parameter/class variable
            //you can change in the editor rather than a random number
            if(doubleJump==1){
                canJump = false;
            }
            lastJump = false;
            doubleJump++;
            
        }
        else if (!canJump && myRig.velocity.y == 0 && doubleJump == 2)
        {
            RaycastHit2D check = Physics2D.CircleCast(this.transform.position, .2f, this.transform.up * -1);
            if (check.distance < 0.2f)
            {
                //out means that whatever variable is inputted next to it will be populated by the function it is in
                    canJump = true;
                    myAnim.SetInteger("DIR", 0);
                    doubleJump = 0;

            }
            //^^^ These two if statements are probably the safest way to perform jump checks

        }


    }
}
