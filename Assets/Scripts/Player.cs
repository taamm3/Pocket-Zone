using UnityEngine;

public class Player : Character
{
    public bool IsAlive { get; private set; } = true;
    private const string ITEM = "ITEM";
    
    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(ITEM))
        {
            TakeItem(collision.gameObject);
        }
    }

    protected override void Attack() { }

    protected override void Die()
    {
        IsAlive = false;
        Debug.Log("Game over");
    }

    private void TakeItem(GameObject item)
    {
        //TODO: ADD ITEM TO INVENTORY
    }
}
