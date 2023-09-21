using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    public Vector3 offset;
    private float smoothTime = 2f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;

    public Transform Target { get => target; set => target = value; }
    GameObject player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }
    private void Start()
    {
        player = GameManager.Instance.player.gameObject;
        
    }
    private void LateUpdate()
    {
        if (player != null)
        {
            if (player.transform.position.y <= -15f)
            {
                offset.x = -10f;
            }
            if (GameManager.Instance.isCameraMainMoving)
            {
                player.GetComponent<Player>().speed = 0;
            }
            else
            {
                player.GetComponent<Player>().speed = 15;
                Vector3 targetPosition = target.position + offset;
                //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime * Time.deltaTime);
                transform.position = Vector3.Lerp(transform.position, targetPosition, smoothTime * Time.deltaTime);
            }
        }
    }

}
