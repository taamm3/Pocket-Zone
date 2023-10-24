using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private GameObject _textBackground;
    [SerializeField] private GameObject _deleteButton;
    private int _itemID;

    public void Init(Item item = null)
    {
        _image.sprite = item.Sprite;
        _itemID = item.ItemID;
        UpdateCount(item.Count);
    }

    public void OnSelect()
    {
        _deleteButton.SetActive(true);
    }

    public void OnDeleteItem()
    {
        GameManager.Instance.RemoveFromBag(_itemID);
        Destroy(gameObject);
    }

    public void UpdateCount(int count)
    {
        if (count > 1)
        {
            _textMeshPro.text = count.ToString();
            _textBackground.SetActive(true);
        }
    }
}
