using UnityEngine;

public class SeedGiver : MonoBehaviour
{
    [SerializeField] private RectTransform BackgroundBar;
    [SerializeField] private RectTransform SeedBar;

    [SerializeField] private GameObject Seed;
    [SerializeField] private int Amount = 30;

    private int _seedsRemaining;

    private void Start()
    {
        _seedsRemaining = Amount;
    }

    private void OnTriggerEnter(Collider collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (!player) return;
        if (player.HasPickable) return;
        if (_seedsRemaining > 0)
        {
            Vector3 newPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            Instantiate(Seed, newPos, transform.rotation);
            _seedsRemaining--;

            SeedBar.sizeDelta = new Vector2(Amount, SeedBar.sizeDelta.y);
        }
    }
}