using UnityEngine;
using UnityEngine.UI;

public class BossEat : MonoBehaviour
{

    [SerializeField] private AudioSource yumyum;
    [SerializeField] private int winScore = 5;
    [SerializeField] private GameObject _uiVictory;
    [SerializeField] private GameObject _loseText;
    [SerializeField] private Text _timerText;
    [SerializeField] private float _timer = 120f;

    private int _score;

    private void Update()
    {
        if (_timer <= 0)
            Lose();

        _timer -= Time.deltaTime;
        _timerText.text = _timer.ToString();
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
        Destroy(collision.gameObject);
        _score++;
        if (_score >= winScore)
        {
            Win();
        }
    }

    private void Win()
    {
        _uiVictory.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Lose()
    {
        _loseText.SetActive(true);
        Time.timeScale = 0f;
    }
}
