using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoTweenPlatforms : MonoBehaviour
{
    public float minX,minY,maxX,maxY;
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.tag == "Player")
    //    {
    //        collision.transform.parent = transform;
    //    }
    //}
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.parent = transform;
            transform.DOLocalMove(new Vector3(maxX, maxY, 0), 4f);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.transform.parent = null;
            DOTween.Sequence().SetDelay(0f).Append(transform.DOLocalMove(new Vector3(minX, minY, 0), 4f));
        }
    }
}
