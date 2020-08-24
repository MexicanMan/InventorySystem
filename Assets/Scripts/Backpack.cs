using UnityEngine;
using UnityEngine.EventSystems;

public class Backpack : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    [Tooltip("UI inventory panel transform")]
    private RectTransform _backpackPanelTransform = null;
    [SerializeField]
    [Tooltip("Content placeholder of UI inventory panel")]
    private RectTransform _backpackContentTransform = null;

    private GameObject _backpackPanel;

    protected void Start()
    {
        _backpackPanel = _backpackPanelTransform.gameObject;
        _backpackPanel.SetActive(false);
    }

    public void OnItemPickedUp(Item item)
    {
        // Put item to the UI inventory panel
        item.transform.SetParent(_backpackContentTransform);
    }

    public void OnItemLaiedOut(Item item)
    {
        // Put item back to world canvas where backpack is
        item.transform.SetParent(transform.parent);
        
        // Change its size and position
        RectTransform itemTransform = item.GetComponent<RectTransform>();
        itemTransform.anchorMin = new Vector2(0.5f, 0.5f);
        itemTransform.anchorMax = new Vector2(0.5f, 0.5f);
        itemTransform.position = new Vector3(Random.Range(-80, 25), Random.Range(-40, 40), 0);
        itemTransform.sizeDelta = new Vector2(item.ImageSize, item.ImageSize);

        // Invoke event
        item.OnItemLaiedOut();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _backpackPanel.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // If there are item in the hovered objects, then lay it out of backpack
        foreach (var obj in eventData.hovered)
        {
            Item item = obj.GetComponent<Item>();
            if (item != null)
            {
                OnItemLaiedOut(item);
                break;
            }
        }

        _backpackPanel.SetActive(false);
    }
}
