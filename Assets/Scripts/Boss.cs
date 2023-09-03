using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

    
    private float timeBtwDamage = 1.5f;


    public Animator camAnim;
    public Slider healthBar;
    private Animator anim;
    public bool isDead;
    public float minX, maxX;
    public int damage;
    public int health;

    public GameObject blood;
    public GameObject deathEffect;
    Transform playerPos;
    private void Start()
    {
        anim = GetComponent<Animator>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {

        if (health <= 2)
        {
            anim.SetTrigger("stageTwo");
        }

        if (health <= 0)
        {
            anim.SetTrigger("death");

        }

        // give the player some time to recover before taking more damage !
        if (timeBtwDamage > 0) {
            timeBtwDamage -= Time.deltaTime;
        }
        
        transform.position = new Vector2(Mathf.Clamp(transform.localPosition.x,minX, maxX),transform.position.y);
        //healthBar.value = health;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // deal the player damage ! 
        if (other.CompareTag("Player") && isDead == false) {
            if (timeBtwDamage <= 0) {
                //camAnim.SetTrigger("shake");
                other.GetComponent<Player>().TakeDamage(1);
            }
        } 
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "Player")
    //    {
    //        collision.GetComponent<Player>().TakeDamage(damage);
    //    }
    //}

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            StartCoroutine(WaitForLevelChange());
        }
        else
        {
            Instantiate(blood, transform.position, Quaternion.identity);
        }
    }
    public IEnumerator WaitForLevelChange()
    {
        yield return new WaitForSeconds(3);
        GameManager.Instance.player.transform.position = Vector3.zero;
        GameManager.Instance.levelManager.levelCount++;
        PlayerPrefs.SetInt("Levels", GameManager.Instance.levelManager.levelCount);
        GameManager.Instance.levelManager.GetLevels();
    }
}
