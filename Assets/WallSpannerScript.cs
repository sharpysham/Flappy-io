using UnityEngine;

public class WallSpannerScript : MonoBehaviour
{
    public GameObject pipe;
    public float spanrate = 2;
    public float timer = 0;
    private float offset = 3 ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spanner();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spanrate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spanner();
            timer = 0;
        }
    }

    void spanner()
    {
        float lowestPoint = transform.position.y - offset;
        float highestPoint = transform.position.y + offset;
        Instantiate(pipe, new Vector3(transform.position.x,Random.Range(lowestPoint,highestPoint),0), transform.rotation);
    }
}
