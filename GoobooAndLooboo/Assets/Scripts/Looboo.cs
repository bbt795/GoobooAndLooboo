using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Looboo : MonoBehaviour
{
    public Animator myAnim;
    public SpriteRenderer myRenderer;
    public Rigidbody2D myRig;

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

    // Start is called before the first frame update
    void Start()
    {
        myAnim = this.GetComponent<Animator>();
        myRenderer = this.GetComponent<SpriteRenderer>();
        myRig = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {


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
