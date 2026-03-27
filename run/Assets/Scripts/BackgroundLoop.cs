using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    public float width;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -width)
        {
            transform.position += new Vector3(width, 0f, 0f);
        }
    }
}
