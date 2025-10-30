using UnityEngine;

public class Food : MonoBehaviour
{
    public int foodValue = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.playerFood += foodValue;
            gameObject.SetActive(false);
        }
    }
}
