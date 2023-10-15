using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public GameObject fadePanel,gameOverPanel,pausePanel,winPanel,uiPanel;
    public Heart hearts;
    public int hubValue;
    public TMP_Text hubValueTextMainMenu, hubValueTextGameOver;
    public GameObject finalDoor,finalPlatform;
    private void Awake()
    {
        Instance = this;
        player = Instantiate(playerPrefab,transform.position,transform.rotation).transform;
        hubValue = PlayerPrefs.GetInt("HubValue",3);
        hubValueTextMainMenu.text = "Lives: " + hubValue;
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
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
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
    public IEnumerator WaitForLevelChange()
    {
        FadePanel();
        PlayerPrefs.SetInt("HubValue", 3);
        GameManager.Instance.hubValueTextGameOver.text = "Lives: " + PlayerPrefs.GetInt("HubValue");
        GameManager.Instance.hubValue = 3;
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.player.transform.position = Vector3.zero;
        GameManager.Instance.levelManager.levelCount++;
        PlayerPrefs.SetInt("Levels", GameManager.Instance.levelManager.levelCount);
        GameManager.Instance.levelManager.GetLevels();
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public void ResumeButton()
    {
        Time.timeScale = 1.0f;
        pausePanel.SetActive(false);
    }
}
