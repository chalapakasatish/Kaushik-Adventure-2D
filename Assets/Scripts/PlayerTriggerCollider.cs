using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Coin":
                CurrencyManager.instance.AddCurrency(1);
                Instantiate(Player.Instance.coinPickupEffect, transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
                break;
            case "Heart":
                Player.Instance.health += 1;
                Instantiate(Player.Instance.blood, transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
                Player.Instance.TookDamagePlayer();
                break;
            case "Boundry":
                Player.Instance.TakeDamage(1);
                Player.Instance.Die();
                break;
            case "Lever1":
                collision.GetComponent<Animator>().Play("GearForward");
                collision.GetComponent<BoxCollider2D>().enabled = false;
                GameManager.Instance.isCameraMainMoving = true;
                Player.Instance.TouchButtonsDeactivate();
                DOTween.Sequence().SetDelay(1f).Append(Player.Instance.cameraMain.transform.DOLocalMove(new Vector3(17.82f, 25f, -30.2334f), 3f)).OnComplete(() =>
                {
                    DOTween.Sequence().SetDelay(0.1f).Append(GameManager.Instance.movingPlatform1.transform.DOLocalMove(new Vector3(33, -2f, 0), 2f)).OnComplete(() =>
                    {
                        Player.Instance.cameraMain.transform.DOLocalMove(new Vector3(CameraController.Instance.Target.position.x,
                            CameraController.Instance.Target.position.y) + CameraController.Instance.offset, 3f).OnComplete(() =>
                            {
                                GameManager.Instance.isCameraMainMoving = false;
                                Player.Instance.TouchButtonsActivate();
                            });
                        DOTween.Sequence().SetDelay(10f).Append(GameManager.Instance.movingPlatform1.transform.DOLocalMove(new Vector3(5, -2f, 0), 3f)).OnComplete(() =>
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
                Player.Instance.TouchButtonsDeactivate();
                DOTween.Sequence().SetDelay(1f).Append(Player.Instance.cameraMain.transform.DOLocalMove(new Vector3(147.4f, 26.5f, -30.2334f), 1f)).OnComplete(() =>
                {
                    DOTween.Sequence().SetDelay(0.1f).Append(GameManager.Instance.movingPlatform2.transform.DOLocalMove(new Vector3(15, -20f, 0), 3f)).OnComplete(() =>
                    {
                        Player.Instance.cameraMain.transform.DOLocalMove(new Vector3(CameraController.Instance.Target.position.x,
                            CameraController.Instance.Target.position.y) + CameraController.Instance.offset, 2f).OnComplete(() =>
                            {
                                GameManager.Instance.isCameraMainMoving = false;
                                Player.Instance.TouchButtonsActivate();
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
                Player.Instance.TouchButtonsDeactivate();
                DOTween.Sequence().SetDelay(1f).Append(Player.Instance.cameraMain.transform.DOLocalMove(new Vector3(180f, 32f, -30.2334f), 2f)).OnComplete(() =>
                {
                    DOTween.Sequence().SetDelay(0.1f).Append(GameManager.Instance.movingPlatform2.transform.DOLocalMove(new Vector3(15, -20f, 0), 3f)).OnComplete(() =>
                    {
                        Player.Instance.cameraMain.transform.DOLocalMove(new Vector3(CameraController.Instance.Target.position.x,
                            CameraController.Instance.Target.position.y) + CameraController.Instance.offset, 3f).OnComplete(() =>
                            {
                                GameManager.Instance.isCameraMainMoving = false;
                                Player.Instance.TouchButtonsActivate();
                            });
                        DOTween.Sequence().SetDelay(20f).Append(GameManager.Instance.movingPlatform2.transform.DOLocalMove(new Vector3(15, 15f, 0), 5f)).OnComplete(() =>
                        {
                            ResetGearPosition(collision.gameObject);
                        });
                    });
                });
                break;
            case "Key1":
                Destroy(collision.gameObject);
                Destroy(GameManager.Instance.door1);
                break;
        }
    }
    public void ResetGearPosition(GameObject collision)
    {
        collision.GetComponent<Animator>().Play("GearBackward");
        collision.GetComponent<BoxCollider2D>().enabled = true;
    }
}