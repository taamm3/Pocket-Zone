using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] MonsterCreator _monsterCreator;
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
}
