using UnityEngine;
using UnityEngine.UI;

public class BossEat : MonoBehaviour
{

    [SerializeField] private AudioSource yumyum;
    [SerializeField] private int winScore = 5;

    private int _score;

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
        GameManager.Instance.ItemsOnGround.Remove(collision.gameObject);
        Destroy(collision.gameObject);
        _score++;
        if (_score >= winScore)
        {
            GameManager.Instance.Victory();
        }
    }
}
