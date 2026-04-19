using UnityEngine;

public class WallScript : MonoBehaviour
{
    public float moveSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;

        if(transform.position.x < -13)
        {
            Debug.Log("Wall destroyed");
            Destroy(gameObject);
        }
    }
}
