using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public static GameManager Instance;
    [Header("Checkpoints")]
    public Vector3 lastCheckpointPosition;
    public Transform player;
    public float x;
    public float y;
    public float z;
    public bool isCameraMainMoving;
    public GameObject rightLeftButtons, jumpButton, attackButton;
    public GameObject movingPlatform1, movingPlatform2,door1,movingLongPillar1,door2;
    public bool isCheckPointsEnable;
    public LevelManager levelManager;
    public GameObject fadePanel,gameOverPanel;
    public Heart hearts;

    private void Awake()
    {
        Instance = this;
        
        player = Instantiate(playerPrefab,transform.position,transform.rotation).transform;

    }
    private void Start()
    {
        if (isCheckPointsEnable)
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
    public void FadePanel()
    {
        fadePanel.gameObject.GetComponent<Animator>().SetTrigger("FadePanel");
    }
    public void ReplayGame() 
    {
        RespawnPlayer(player.gameObject);
        FadePanel();
    }
    public void PlayGame()
    {
        FadePanel();
    }
}
