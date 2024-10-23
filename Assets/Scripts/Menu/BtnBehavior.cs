using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BtnBehavior : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        MenuManager.Instance.eventSoundSource.PlayOneShot(MenuManager.Instance.hoverSound);
    }
}
