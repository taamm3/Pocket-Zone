using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private GameObject _textBackground;

    public void Init(Item item = null)
    {
        _image.sprite = item.Sprite;
        UpdateCount(item.Count);
    }

    public void OnSelect()
    {

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
