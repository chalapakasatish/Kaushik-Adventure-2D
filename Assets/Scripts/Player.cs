using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using SimpleInputNamespace;
public class Player : MonoBehaviour
{

    public float speed;
    Rigidbody2D rb;
    bool facingRight = true;

    bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float jumpForce;

    bool isTouchingFront;
    public Transform frontCheck;
    bool wallJumping;
    public float wallJumpTime;
    public float xWallForce;
    public float yWallForce;
    bool wallSliding;
    public float wallSlidingSpeed;

    Animator anim;

    public int health;

    public float timeBetweenAttacks;
    float nextAttackTime;

    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayer;

    public int damage;

    public SpriteRenderer weaponRenderer;

    public GameObject blood, deathEffect, pickupEffect, swordSwingEffect, coinPickupEffect, dropEffect;

    AudioSource source;

    public AudioClip jumpSound;
    public FloatingJoystick floatingjoystick;
    public bool isAttack,isJump,isMovingRight,isMovingLeft;
    public Heart hearts;
    public float input;
    public Vector3 lastPosition;
    GameObject cameraMain;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        cameraMain = GameObject.FindGameObjectWithTag("MainCamera");

    }

    private void FixedUpdate()
    {
        input = SimpleInput.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(input * speed, rb.velocity.y);
        if (Time.time > nextAttackTime)
        {
            if (Input.GetKey(KeyCode.Space) || isAttack)
            {
                anim.SetTrigger("attack");
                nextAttackTime = Time.time + timeBetweenAttacks;
            }
        }

        if (input > 0 && facingRight == false)
        {
            Flip();
        } else if (input < 0 && facingRight == true) {
            Flip();
        }

        if (input != 0)
        {
            anim.SetBool("isRunning", true);
        } else {
            anim.SetBool("isRunning", false);
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);

        if (isGrounded == true)
        {
            anim.SetBool("isJumping", false);
        } else {
            anim.SetBool("isJumping", true);
        }

        if (Input.GetKeyDown(KeyCode.W) && isGrounded == true || Input.GetKeyDown(KeyCode.UpArrow) && isGrounded == true || isJump == true && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
            source.clip = jumpSound;
            source.Play();
            isJump = false;
        }
        

        //if (isTouchingFront && !isGrounded && input != 0)
        //{
        //    wallSliding = true;
        //} else {
        //    wallSliding = false;
        //}

        //if (wallSliding)
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        //}

        //if (Input.GetKeyDown(KeyCode.W) && wallSliding || isJump == true && wallSliding)
        //{
        //    wallJumping = true;
        //    Invoke("SetWallJumpingToFalse", wallJumpTime);
        //}

        //if (wallJumping)
        //{
        //    rb.velocity = new Vector2(xWallForce * -input, yWallForce);
        //    source.clip = jumpSound;
        //    source.Play();
        //}


    }

    public void AttackButtonUp()
    {
        isAttack = false;
    }
    public void AttackButtonDown()
    {
        isAttack = true;
    }
    public void JumpButtonUp()
    {
        isJump = false;
    }
    public void JumpButtonDown()
    {
        isJump = true;
    }
    void Flip() {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingRight = !facingRight;
    }

    void SetWallJumpingToFalse() {
        wallJumping = false;
    }

    public void TakeDamage(int damage) {
        //FindObjectOfType<CameraShake>().Shake();
        health -= damage;
        TookDamagePlayer();
        if (health <= 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            SceneManager.LoadScene(0);
        } else {
            Instantiate(blood, transform.position, Quaternion.identity);
        }
    }

    public void Attack() {

        Instantiate(swordSwingEffect, attackPoint.position, Quaternion.identity);
        //FindObjectOfType<CameraShake>().Shake();
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D col in enemiesToDamage)
        {
            switch (col.tag)
            {
                case "Enemy":
                    col.GetComponent<Enemy>().TakeDamage(damage);
                    break;
                case "Boss1":
                    col.GetComponent<Boss>().TakeDamage(damage);
                    break;
            }
        }
        isAttack = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void Equip(Weapon weapon) {
        damage = weapon.damage;
        attackRange = weapon.attackRange;
        weaponRenderer.sprite = weapon.GFX;
        Instantiate(pickupEffect, transform.position, Quaternion.identity);
        Destroy(weapon.gameObject);
    }

    public void Land() {
        Vector2 pos = new Vector2(groundCheck.position.x, groundCheck.position.y + 1);
        Instantiate(dropEffect, pos, Quaternion.identity);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Coin":
                CurrencyManager.instance.AddCurrency(1);
                Instantiate(coinPickupEffect, transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
                break;
            case "Heart":
                health += 1;
                Instantiate(blood, transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
                TookDamagePlayer();
                break;
            case "Boundry":
                TakeDamage(1);
                Die();
                break;
            case "Lever1":
                collision.GetComponent<Animator>().Play("GearForward");
                collision.GetComponent<BoxCollider2D>().enabled = false;
                GameManager.Instance.isCameraMainMoving = true;
                TouchButtonsDeactivate();
                DOTween.Sequence().SetDelay(1f).Append(cameraMain.transform.DOLocalMove(new Vector3(17.82f, 25f, -30.2334f), 3f)).OnComplete(() =>
                {
                    DOTween.Sequence().SetDelay(0.1f).Append(GameManager.Instance.movingPlatform1.transform.DOLocalMove(new Vector3(33, -2f, 0), 2f)).OnComplete(() =>
                    {
                        cameraMain.transform.DOLocalMove(new Vector3(CameraController.Instance.Target.position.x,
                            CameraController.Instance.Target.position.y) + CameraController.Instance.offset, 3f).OnComplete(() =>
                        {
                            GameManager.Instance.isCameraMainMoving = false;
                            TouchButtonsActivate();
                        });
                        DOTween.Sequence().SetDelay(13f).Append(GameManager.Instance.movingPlatform1.transform.DOLocalMove(new Vector3(5, -2f, 0), 8f)).OnComplete(() =>
                        {
                            ResetGearPosition(collision.gameObject);
                        });
                    });
                });
                break;
            case "Lever2":
                collision.GetComponent<Animator>().Play("GearForward");
                collision.GetComponent<BoxCollider2D>().enabled = false;
                GameManager.Instance.isCameraMainMoving = true;
                TouchButtonsDeactivate();
                DOTween.Sequence().SetDelay(1f).Append(cameraMain.transform.DOLocalMove(new Vector3(147.4f, 26.5f, -30.2334f), 1f)).OnComplete(() =>
                {
                    DOTween.Sequence().SetDelay(0.1f).Append(GameManager.Instance.movingPlatform2.transform.DOLocalMove(new Vector3(15, -20f, 0), 3f)).OnComplete(() =>
                    {
                        cameraMain.transform.DOLocalMove(new Vector3(CameraController.Instance.Target.position.x,
                            CameraController.Instance.Target.position.y) + CameraController.Instance.offset, 2f).OnComplete(() =>
                            {
                                GameManager.Instance.isCameraMainMoving = false;
                                TouchButtonsActivate();
                            });
                        DOTween.Sequence().SetDelay(20f).Append(GameManager.Instance.movingPlatform2.transform.DOLocalMove(new Vector3(15, 0f, 0), 3f)).OnComplete(() =>
                        {
                            ResetGearPosition(collision.gameObject);
                        });
                    });
                });
                break;
            case "Lever3":
                collision.GetComponent<Animator>().Play("GearForward");
                collision.GetComponent<BoxCollider2D>().enabled = false;
                GameManager.Instance.isCameraMainMoving = true;
                TouchButtonsDeactivate();
                DOTween.Sequence().SetDelay(1f).Append(cameraMain.transform.DOLocalMove(new Vector3(180f, 32f, -30.2334f), 2f)).OnComplete(() =>
                {
                    DOTween.Sequence().SetDelay(0.1f).Append(GameManager.Instance.movingPlatform2.transform.DOLocalMove(new Vector3(15, -20f, 0), 3f)).OnComplete(() =>
                    {
                        cameraMain.transform.DOLocalMove(new Vector3(CameraController.Instance.Target.position.x,
                            CameraController.Instance.Target.position.y) + CameraController.Instance.offset, 3f).OnComplete(() =>
                            {
                                GameManager.Instance.isCameraMainMoving = false;
                                TouchButtonsActivate();
                            });
                        DOTween.Sequence().SetDelay(20f).Append(GameManager.Instance.movingPlatform2.transform.DOLocalMove(new Vector3(15, 15f, 0), 5f)).OnComplete(() =>
                        {
                            ResetGearPosition(collision.gameObject);
                        });
                    });
                });
                break;
        }
    }
    public void ResetGearPosition(GameObject collision) 
    {
        collision.GetComponent<Animator>().Play("GearBackward");
        collision.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void Die()
    {
        GameManager.Instance.RespawnPlayer(gameObject);
    }
    public void TookDamagePlayer()
    {
        if (health > hearts.numberOfHearts)
        {
            health = hearts.numberOfHearts;
        }

        for (int i = 0; i < hearts.hearts.Length; i++)
        {
            if (i < hearts.numberOfHearts)
            {
                hearts.hearts[i].enabled = true;
            }
            else
            {
                hearts.hearts[i].enabled = false;
            }

            if (i < health)
            {
                hearts.hearts[i].sprite = hearts.fullHeart;
            }
            else
            {
                hearts.hearts[i].sprite = hearts.brokenHeart;
            }

        }
    }
    private void OnApplicationFocus(bool focus)
    {
        lastPosition = gameObject.transform.position;
    }
    public void TouchButtonsActivate()
    {
        GameManager.Instance.rightLeftButtons.SetActive(true);
        GameManager.Instance.jumpButton.SetActive(true);
        GameManager.Instance.attackButton.SetActive(true);
    } public void TouchButtonsDeactivate()
    {
        GameManager.Instance.rightLeftButtons.SetActive(false);
        GameManager.Instance.jumpButton.SetActive(false);
        GameManager.Instance.attackButton.SetActive(false);
    }
}
