using UnityEngine;

public class CloudMove : MonoBehaviour
{
    public float speed;

    void Start()
    {
        // random slow speed for depth effect
        speed = Random.Range(0.5f, 2f);
    }

    void Update()
    {
        // move left
        transform.position += Vector3.left * speed * Time.deltaTime;

        // destroy when out of screen
        if (transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }
}