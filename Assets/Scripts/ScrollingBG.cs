using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBG : MonoBehaviour
{
    public float scrollSpeed = 1.0f;
    Vector3 scrollStartPosition;
    // Start is called before the first frame update
    void Start()
    {
        scrollStartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);
        if (transform.position.y < -4.43f)
        {
            transform.position = scrollStartPosition;
        }
    }
}
