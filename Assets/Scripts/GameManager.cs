using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField] private GameObject VictoryText;
    [SerializeField] private GameObject LoseText;
    [SerializeField] private Text TimerText;
    [SerializeField] private float TimeLimit = 120f;

    public List<GameObject> ItemsOnGround;
    [HideInInspector] public bool HasRemainingSeed1 = true;
    [HideInInspector] public bool HasRemainingSeed2 = true;

    [SerializeField] private Plant[] Farmlands;
    private bool _seedGrowing = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        ItemsOnGround = new List<GameObject>();
    }

    private void Update()
    {
        if (TimeLimit <= 0)
            Defeat();

        TimeLimit -= Time.deltaTime;
        int timer = (int)TimeLimit;
        TimerText.text = timer.ToString();

        CheckForSeedsGrowing();

        if (TimeLimit <= 0 || (!HasRemainingSeed1 && !HasRemainingSeed2 && ItemsOnGround.Count == 0 && !_seedGrowing))
            Defeat();
    }

    public void Victory()
    {
        VictoryText.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Defeat()
    {
        LoseText.SetActive(true);
        Time.timeScale = 0f;
    }

    private void CheckForSeedsGrowing()
    {
        foreach (Plant farmland in Farmlands)
        {
            if (farmland.IsSeeded)
            {
                _seedGrowing = true;
                return;
            }
            else
                _seedGrowing = false;
        }
    }
}
