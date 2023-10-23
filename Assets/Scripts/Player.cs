using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    [SerializeField] private Button _fireButton;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Weapon _weapon;
    public bool IsAlive { get; private set; } = true;
    private const string ITEM = "Item";
    private const string MONSTER = "Monster";
    private string _filePath;

    private void OnEnable()
    {
        _fireButton.onClick.AddListener(Attack);
        _filePath = Application.dataPath + "/PlayerDataFile.json";
        if (File.Exists(_filePath))
        {
            LoadFromJson();
            FillHealthBar();
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

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(_filePath, json);
    }

    public void LoadFromJson()
    {
        string json = File.ReadAllText(_filePath);
        CharacterData data = JsonUtility.FromJson<CharacterData>(json);

        transform.position = data.Position;
        _health = data.Health;
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
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        int monsterLayerMask = 1 << LayerMask.NameToLayer(MONSTER);
        Collider2D[] hits = Physics2D.OverlapCircleAll(bullet.transform.position, 0.1f, monsterLayerMask);
        foreach (Collider2D hit in hits)
        {
            Monster monster = hit.gameObject.GetComponent<Monster>();
            monster.TakeDamage(_weapon.DamageAmount);
        }
        Destroy(bullet, 2f);
    }

    protected override void Die()
    {
        IsAlive = false;
        Destroy(gameObject);
        Debug.Log("Game over");
    }

    private void TakeItem(GameObject item)
    {
        //TODO: ADD ITEM TO INVENTORY
    }
}
