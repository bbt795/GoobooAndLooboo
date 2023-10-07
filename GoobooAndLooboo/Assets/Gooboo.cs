using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gooboo : MonoBehaviour
{
    public Animator myAnim;
    public SpriteRenderer myRenderer;
    public Rigidbody myRig;

    public bool canJump = true;
    public bool lastJump = false;

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
        myRig = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.A))
        {

            if (myRenderer != null)
            {

                myRenderer.flipX = true;
                myAnim.SetInteger("DIR", 1);

            }

        }

        if (Input.GetKeyDown(KeyCode.D))
        {

            if (myRenderer != null)
            {

                myRenderer.flipX = false;
                myAnim.SetInteger("DIR", 1);

            }
        }

        //myRig.velocity = transform.forward * speed * lastDirection.y + new Vector3(0, myRig.velocity.y, 0);
        myRig.velocity = new Vector3(lastDirection.x, 0, lastDirection.y).normalized * speed;

        if (lastJump)
        {
            myRig.velocity += new Vector3(0, 8, 0);
            //this is poor practice, it is better to have a parameter/class variable
            //you can change in the editor rather than a random number
            lastJump = false;
            canJump = false;
        }
        else if (!canJump && myRig.velocity.y <= 0)
        {
            RaycastHit check;
            if (Physics.SphereCast(this.transform.position, .2f, this.transform.up * -1, out check))
            {
                //out means that whatever variable is inputted next to it will be populated by the function it is in
                if (check.distance < .6f)
                {
                    canJump = true;
                    myAnim.SetInteger("DIR", 0);

                }
            }
            //^^^ These two if statements are probably the safest way to perform jump checks

        }


    }
}
