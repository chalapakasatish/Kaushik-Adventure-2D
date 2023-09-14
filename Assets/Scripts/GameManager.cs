using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("Checkpoints")]
    public Vector3 lastCheckpointPosition;
    public Transform player;
    public float x;
    public float y;
    public float z;
    public bool isCameraMainMoving;
    public GameObject rightLeftButtons, jumpButton, attackButton;
    public GameObject movingPlatform1, movingPlatform2,door1;
    public bool isCheckPointsEnable;
    public LevelManager levelManager;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void Start()
    {
        if(isCheckPointsEnable)
        {
            x = PlayerPrefs.GetFloat("x");
            y = PlayerPrefs.GetFloat("y");
            z = PlayerPrefs.GetFloat("z");
            player.transform.position = new Vector3(x, y, z);
        }
    }

    public void UpdateCheckpoint(Vector2 position)
    {
        lastCheckpointPosition = position;
        
        x = lastCheckpointPosition.x;
        PlayerPrefs.SetFloat("x", x);
        y = lastCheckpointPosition.y;
        PlayerPrefs.SetFloat("y", y);
        z = lastCheckpointPosition.z;
        PlayerPrefs.SetFloat("z", z);
    }

    public void RespawnPlayer(GameObject player)
    {
        player.transform.position = lastCheckpointPosition;
        // You might also want to reset player's health, score, etc. here.
    }
}
