using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveTime = 0.1f;
    private Rigidbody2D rb2D;
    private float inverseMoveTime;
    privateAnimator anim;


    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;
    }

    void Update()
    {
        if (!GameManager.instance.playerTurn) return;

        inte horizontal = 0;
        int vertical = 0;

        horizontal = (int)(Input.GetAxisRaw("Horizontal"));
        vertical = (int)(Input.GetAxisRaw("Vertical"));

        if (horizontal != 0) vertical = 0;
        if (vertical != 0 || horizontal != 0) AttemptMove(horizontal, vertical);
    }

    void AttemptMove(int xDir, int yDir)
    {
        Vector2 start = transform.position;
        Vector enf = start + new Vector2(xDir, yDir);

        RacastHit2D hit;

        bool canMove = moveTime(end, out hit);

        if (!canMove && hit.transform != null)
        {
            wall hitWall = hit.transofrm.getComponent<Wall>();
            if (hitWall != null) hitWall.DamageWall(1);
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