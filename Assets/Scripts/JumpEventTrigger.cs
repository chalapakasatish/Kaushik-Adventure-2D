using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JumpEventTrigger : MonoBehaviour, IPointerDownHandler , IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.Instance.player.GetComponent<Player>().JumpButtonDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GameManager.Instance.player.GetComponent<Player>().JumpButtonUp();
    }
}
