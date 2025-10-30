unity UsingEngine;

public class EnemyController : MonoBehaviour
{
    public int hp = 2;
    public int damage = 5;
    public float moveTime = 0.1f;
    private Rigidbody2D rb;

    private Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeTurn(Vector2 playerPos)
    {
        moveDirection = GetMoveDirection(playerPos);
        AttemptMove(moveDirection);
    }

    Vector2 GetMoveDirection(Vector2 playerPos)
    {
        Vector2 dir = Vector2.zero;
        flaot xDiff = playerPos.x - transform.position.x;
        float yDiff = playerPos.y - transform.position.y;
        if (Mathf.Abs(xDiff) > Mathf.Abs(yDiff))
        {
            dir.x = xDiff > 0 ? 1 : -1;
        }
        else
        {
            dir.y = yDiff > 0 ? 1 : -1;
        }
        return dir;
    }

    void AttemptMove(Vector2 direction)
    {
        Vector2 start = rb2D.position;
        Vector2 end = start + direction;

        RaycastHit2D hit = Physics2D.Linecast(start, end);

        if (hit.transform == null)
        {
            // Move to empty space
            rb2D.MovePosition(end);
        }
        else
        {
            // Check if player is hit
            PlayerController player = hit.transform.GetComponent<PlayerController>();
            if (player != null)
            {
                GameManager.instance.playerFood -= damage;
                if (GameManager.instance.playerFood <= 0)
                    GameManager.instance.GameOver();
            }
        }
    }
    
    public void TakeDamage(int amount)
    {
        hp -= amount;
        if (hp <= 0)
            Destroy(gameObject);
    }

}