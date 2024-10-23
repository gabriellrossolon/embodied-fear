using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BtnBehavior : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (MenuManager.Instance != null)
        {
            MenuManager.Instance.eventSoundSource.PlayOneShot(MenuManager.Instance.hoverSound);
        }
        else
        {
            SoundManager.Instance.eventsSoundSource.PlayOneShot(SoundManager.Instance.hoverSound);
        }
    }
}
