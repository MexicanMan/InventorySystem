using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent<Item> OnPickUp;
    public UnityEvent<Item> OnLayDown;

    [SerializeField]
    private int _id = 0;
    [SerializeField]
    private string _name = "";
    [SerializeField]
    private double _weight;

    [SerializeField]
    private float _imageSize = 0;

    public int Id { get { return _id; } }
    public float ImageSize { get { return _imageSize; } }

    protected void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(_imageSize, _imageSize);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnPickUp.Invoke(this);
    }

    public void OnItemLaiedOut()
    {
        OnLayDown.Invoke(this);
    }    
}
