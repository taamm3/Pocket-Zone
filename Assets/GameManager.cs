using UnityEngine;
using UnityEngine.UI;

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
        if (_player.IsAlive)
        {
            return _player.transform.position;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public void RemoveFromBag(int id)
    {
        _player.RemoveFromBag(id);
    }
}
