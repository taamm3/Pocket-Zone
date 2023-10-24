using System.Collections;
using UnityEngine;

public class Monster : Character
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private int _damageAmount = 5;
    [SerializeField] private GameObject _item;
    private Player _player;
    private const float DELAY = 5f;

    void Update()
    {
        if (CheckMonsterVisibility())
        {
            Attack();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _player = collision.gameObject.GetComponent<Player>();

        if (_player && _player.IsAlive)
        {
            StartCoroutine(ContinuousAttack(DELAY));
        }
    }

    private IEnumerator ContinuousAttack(float delay)
    {
        while (_player && _player.IsAlive)
        {
            _player.TakeDamage(_damageAmount);
            yield return new WaitForSeconds(delay);
        }
    }

    private bool CheckMonsterVisibility()
    {
        Vector3 screenPoint = _camera.WorldToViewportPoint(transform.position);
        return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 &&
               screenPoint.y > 0 && screenPoint.y < 1;
    }

    protected override void Attack()
    {
        if (!_player || _player.IsAlive)
        {
            StartCoroutine(StartMovingToPlayer());
        }
    }

    private IEnumerator StartMovingToPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            GameManager.Instance.GetPlayerPosition(), _speed * Time.deltaTime);
        yield return null;
    }

    protected override void Die()
    {
        Instantiate(_item, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void SetCameraReference(Camera camera)
    {
        _camera = camera;
    }
}
