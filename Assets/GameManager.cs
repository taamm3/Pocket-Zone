using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] MonsterCreator _monsterCreator;
    [SerializeField] Player _player;
    public static GameManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _monsterCreator.GenerateMonsters();
    }

    public Vector3 GetPlayerPosition()
    {
        return _player.transform.position;
    }
}
