using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.Progress;

public class Player : Character
{
    [SerializeField] private Button _fireButton;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private TextMeshProUGUI _bulletsText;
    [SerializeField] private int _bulletCount = 60;
    public bool IsAlive { get; private set; } = true;
    private const string ITEM = "Item";
    private const string MONSTER = "Monster";
    private const string BULLETS = "Bullets:";
    private const int MAX_HEALTH = 100;
    private const int MAX_BULLET_COUNT = 60;
    private string _filePath;

    private void OnEnable()
    {
        _fireButton.onClick.AddListener(Attack);
        _filePath = Application.dataPath + "/PlayerDataFile.json";
        if (File.Exists(_filePath))
        {
            LoadFromJson();
            FillHealthBar();
            UpdateBulletCountText();
        }
    }

    private void OnDisable()
    {
        _fireButton.onClick.RemoveListener(Attack);
        SaveToJson();
    }

    public void SaveToJson()
    {
        CharacterData data = new CharacterData();
        data.Position = transform.position;
        data.Health = _health;
        data.BulletCount = _bulletCount;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(_filePath, json);
    }

    public void LoadFromJson()
    {
        string json = File.ReadAllText(_filePath);
        CharacterData data = JsonUtility.FromJson<CharacterData>(json);

        transform.position = data.Position;
        _health = data.Health;
        _bulletCount = data.BulletCount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(ITEM))
        {
            TakeItem(collision.gameObject);
        }
    }

    protected override void Attack()
    {
        if (_bulletCount > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            int monsterLayerMask = 1 << LayerMask.NameToLayer(MONSTER);
            Collider2D[] hits = Physics2D.OverlapCircleAll(bullet.transform.position, 0.1f, monsterLayerMask);
            foreach (Collider2D hit in hits)
            {
                Monster monster = hit.gameObject.GetComponent<Monster>();
                monster.TakeDamage(_weapon.DamageAmount);
            }
            Destroy(bullet, 2f);
            _bulletCount--;
            UpdateBulletCountText();
        }
    }

    private void UpdateBulletCountText()
    {
        _bulletsText.text = BULLETS + _bulletCount.ToString();
    }

    protected override void Die()
    {
        IsAlive = false;
        Destroy(gameObject);
        Debug.Log("Game over");
        _health = MAX_HEALTH;
        _bulletCount = MAX_BULLET_COUNT;
        SaveToJson();
    }

    private void TakeItem(GameObject obj)
    {
        Item item = obj.GetComponent<Item>();
        _inventory.AddItem(item);
        Destroy(obj);
    }

    public void RemoveFromBag(int id)
    {
        _inventory.DeleteItem(id);
    }
}
