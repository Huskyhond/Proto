using UnityEngine;

public class SeedGiver : MonoBehaviour
{
    [SerializeField] private int SeedGiverId;
    [SerializeField] private RectTransform BackgroundBar;
    [SerializeField] private RectTransform SeedBar;

    [SerializeField] private GameObject Seed;
    [SerializeField] private int Amount = 30;

    private GameManager gameManager;
    private int _seedsRemaining;

    private void Start()
    {
        gameManager = GameManager.Instance;
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
            GameObject seed = Instantiate(Seed, newPos, transform.rotation);
            gameManager.ItemsOnGround.Add(seed);
            _seedsRemaining--;

            float percentage = _seedsRemaining * 1.0f / Amount * 100;
            SeedBar.sizeDelta = new Vector2(percentage, SeedBar.sizeDelta.y);

            if (_seedsRemaining == 0)
            {
                if (SeedGiverId == 1)
                    gameManager.HasRemainingSeed1 = false;
                if (SeedGiverId == 2)
                    gameManager.HasRemainingSeed2 = false;
            }
        }
    }
}