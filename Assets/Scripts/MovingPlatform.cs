using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public Transform[] patrolPoints;
    public float speed;
    int currentPointIndex;

    float waitTime;
    public float startWaitTime;
    public bool startMoving;

    private void Start()
    {
        transform.position = patrolPoints[0].position;
        waitTime = startWaitTime;
    }

    private void Update()
    {
        if (startMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);
            if (transform.position == patrolPoints[currentPointIndex].position)
            {
                if (waitTime <= 0)
                {
                    if (currentPointIndex + 1 < patrolPoints.Length)
                    {
                        currentPointIndex++;
                    }
                    else
                    {
                        currentPointIndex = 0;
                    }
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }

            }
        }
        
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "Player")
    //    {
    //        collision.transform.parent = transform;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.tag == "Player")
    //    {
    //        collision.transform.parent = null;
    //    }
    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.transform.parent = transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.transform.parent = null;
        }
    }


}
