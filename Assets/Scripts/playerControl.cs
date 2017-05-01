using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    public AudioClip jumpSound;
    public AudioClip landedSound;
    public AudioClip stepSound;

    public float topSpeed = 10f;

    bool facingRight = true;

    Animator anim;

    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.1f;
    public LayerMask whatIsGround;
    public float jumpForce = 250f;
    bool landing = false;
    public Transform landingCheck;


    private AudioSource source;
    private float volVeryLowRange = .01f;
    private float volLowRange = .1f;
    private float volHighRange = 1.0f;

    float offSet;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(jumpSound, vol);
            anim.SetTrigger("canJump");
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
            anim.SetBool("grounded", false);
        }
	}

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("grounded", grounded);
        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

        landing = Physics2D.OverlapCircle(landingCheck.position, groundRadius, whatIsGround);
        anim.SetBool("landing", landing);
        //if(!grounded) topSpeed = 5f;

        float move = Input.GetAxis("Horizontal");
        //Debug.Log(move);
        if (grounded)
        {

            GetComponent<Rigidbody2D>().velocity = new Vector2(move * topSpeed, GetComponent<Rigidbody2D>().velocity.y);
            anim.SetFloat("Speed", Mathf.Abs(move));

            if (move > 0 && !facingRight)
                Flip();
            else if (move < 0 && facingRight)
                Flip();
        }
        //topSpeed = 10f;
    }

    void Flip()
    {
        if (facingRight)
            offSet = 1.48f;
        else if (!facingRight)
            offSet = -1.48f;

        facingRight = !facingRight;
        Vector3 thePosition = transform.localPosition;
        thePosition.x -= offSet;
        transform.localPosition = thePosition;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void addJumpForce()
    {
        if(grounded)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
            anim.SetBool("grounded", false);
        }
    }

    void FootStep()
    {
        float vol = Random.Range(volVeryLowRange, volLowRange);
        source.PlayOneShot(stepSound, 0.001f);
        
    }

}
