using UnityEngine;

public class MonsterCreator : MonoBehaviour
{
    [SerializeField] private Transform _ground;
    [SerializeField] private GameObject _monsterPrefab;
    [SerializeField] private Camera _camera;
    private const int NUMBER_OF_MONSTERS = 3;

    public void GenerateMonsters()
    {
        SpriteRenderer area = _ground.GetComponent<SpriteRenderer>();
        for (int i = 0; i < NUMBER_OF_MONSTERS; i++)
        {
            float rectHeight = area.bounds.extents.y;
            float areaWidth = area.bounds.extents.x;
            float xpos = Random.Range(-areaWidth, areaWidth);
            float ypos = Random.Range(-rectHeight, rectHeight);
            Vector3 pos = area.transform.position + new Vector3(xpos, ypos);
            GameObject monster = Instantiate(_monsterPrefab, pos, Quaternion.identity);
            monster.GetComponent<Monster>().SetCameraReference(_camera);
        }
    }
}
