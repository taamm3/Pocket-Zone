using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField] protected Image _healthImage;
    protected float _health = MAX_HEALTH;
    private const float MAX_HEALTH = 100f;

    protected virtual void Attack() { }

    public void TakeDamage(int _damageAmount)
    {
        if (_damageAmount <= 0)
        {
            return;
        }
        _health = Mathf.Clamp(_health - _damageAmount, 0, MAX_HEALTH);
        FillHealthBar();
        if (_health == 0)
        {
            Die();
        }
    }

    protected void FillHealthBar()
    {
        _healthImage.fillAmount = _health / MAX_HEALTH;
    }

    protected virtual void Die() { }
}
