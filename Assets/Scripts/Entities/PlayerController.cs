using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveTime = 0.1f;
    private Rigidbody2D rb2D;
    private float inverseMoveTime;
    private Animator anim;


    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;
    }

    void Update()
    {
        if (!GameManager.instance.playersTurn) return;

        int horizontal = 0;
        int vertical = 0;

        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");

        if (horizontal != 0) vertical = 0;

        if (horizontal != 0 || vertical != 0)
        {
            AttemptMove(horizontal, vertical);
            GameManager.instance.playersTurn = false; // ✅ use "playersTurn"
        }
    }

    void AttemptMove(int xDir, int yDir)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);

        RaycastHit2D hit = Physics2D.Linecast(start, end);

        if (hit.transform == null)
        {
            transform.position = end;
        }
        else
        {
            EnemyController enemy = hit.transform.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(1); // Player attacks enemy
            }
            else
            {
                Wall hitWall = hit.transform.GetComponent<Wall>();
                if (hitWall != null)
                {
                    hitWall.DamageWall(1);
                }
            }
        }

        GameManager.instance.playersTurn = false;
    }


    bool Move(Vector2 end, out RaycastHit2D hit)
    {
        hit = Physics2D.Linecast(transform.position, end);
        if (hit.transform == null)
        {
            transform.position = end;
            return true;
        }
        return false;
    } 
}