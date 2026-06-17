using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float startPos, length;
    public float scrollSpeed = 2f;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float newPos = Mathf.Repeat(Time.time * scrollSpeed, length);
        transform.position = new Vector3(startPos - newPos, transform.position.y, transform.position.z);

    }
}
