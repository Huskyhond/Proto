using UnityEngine;
using UnityEngine.UI;

public class BossEat : MonoBehaviour
{

    [SerializeField] private AudioSource yumyum;

    private GameManager gameManager;
    private int _score;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter(Collider collision)
    {
        Eat(collision);
    }

    private void Eat(Collider collision)
    {
        var pickable = collision.GetComponent<Pickable>();
        if (!pickable) return;
        if (pickable.Type != Pickable.PickableType.PINK_MUSHROOM) return;

        yumyum.Play();
        gameManager.ItemsOnGround.Remove(collision.gameObject);
        Destroy(collision.gameObject);

        gameManager.IncreaseScore();
    }
}
