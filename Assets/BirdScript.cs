using UnityEngine;
using System.Collections;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapstrength = 13;
    public Logic Logic;
    public bool isBirdAlive = true;

    public Animator animator;

    public float maxTiltUp = 25f;
    public float maxTiltDown = -60f;

    

    void Start()
    {
        Logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<Logic>();
        myRigidbody.gravityScale = 3f;
    }

    void Update()
    {
        if (!isBirdAlive) return;

        // 🔥 FLAP INPUT
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody.velocity = new Vector2(0, flapstrength);

            CancelInvoke("RestoreGravity");
            myRigidbody.gravityScale = 1.8f;
            Invoke("RestoreGravity", 0.15f);

            // animation trigger
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Flap"))
            {
                animator.SetTrigger("flap");
            }
        }

        // 🔥 TILT LOGIC
        float velocityY = myRigidbody.velocity.y;

        float targetTilt = velocityY * 3.5f;
        targetTilt = Mathf.Clamp(targetTilt, maxTiltDown, maxTiltUp);

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.Euler(0, 0, targetTilt),
            4f * Time.deltaTime
        );

        // 🔥 DYNAMIC TRAIL
        
    }

    void RestoreGravity()
    {
        myRigidbody.gravityScale = 3f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Logic.gameOver();
        isBirdAlive = false;
    }

    private void OnBecameInvisible()
    {
        if (isBirdAlive)
        {
            Logic.gameOver();
            isBirdAlive = false;
        }
    }
}