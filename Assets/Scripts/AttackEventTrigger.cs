using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AttackEventTrigger : MonoBehaviour, IPointerDownHandler , IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.Instance.player.GetComponent<Player>().AttackButtonDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GameManager.Instance.player.GetComponent<Player>().AttackButtonUp();
    }
}
