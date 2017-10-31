using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public bool IsSeeded { get { return _seeded; } }
    private bool _seeded = false;
    [SerializeField] private GameObject _seedlingPrefab;
    [SerializeField] private GameObject _seedType;

    private bool _isFullyGrown = false;
    
    void Start()
    {
        ColorizePatch();
    }

    public virtual void UnSeed()
    {
        _seeded = false;
        transform.localScale = new Vector3(1f, 0.01f, 1f);
        ColorizePatch();
    }

    public void ColorizePatch()
    {
        if (_seedType.GetComponent<Pickable>().Type == Pickable.PickableType.PINK_SEED)
        {
            GetComponent<Renderer>().material.SetColor("_Color", new Color(1, 0, 1));
        }
        else if (_seedType.GetComponent<Pickable>().Type == Pickable.PickableType.GOLD_SEED)
        {
            GetComponent<Renderer>().material.SetColor("_Color", new Color(1, 1, 0));
        }
    }

    public virtual void Seed()
    {
        _seeded = true;
        _isFullyGrown = false;
        transform.localScale = transform.localScale + new Vector3(0f, 0.1f, 0f);

        if (_seedType.GetComponent<Pickable>().Type == Pickable.PickableType.PINK_SEED)
        {
            GetComponent<Renderer>().material.SetColor("_Color", new Color(171f / 255f, 18f / 255f, 159f / 255f));
        }
        else if (_seedType.GetComponent<Pickable>().Type == Pickable.PickableType.GOLD_SEED)
        {
            GetComponent<Renderer>().material.SetColor("_Color", new Color(208f / 255f, 193f / 255f, 57f / 255f));
        }

    }

    public virtual void Grow()
    {
        transform.localScale += new Vector3(0f, 0.1f, 0f);
        transform.localScale = new Vector3(1f, Mathf.Min(transform.localScale.y, 5f), 1f);
        if (transform.localScale.y >= 5f)
        {
            StartCoroutine(StartSeedling());
        }
    }

    IEnumerator StartSeedling()
    {
        _isFullyGrown = true;
        yield return new WaitForSeconds(5f);
        if (transform.localScale.y >= 5f)
        {
            UnSeed();
            var mushroom = Instantiate(_seedlingPrefab, transform.position, Quaternion.identity);
            if (EnemyMushroom.mushrooms != null) {
                EnemyMushroom.mushrooms.Add(mushroom);
            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        var player = collision.GetComponent<Player>();
        if (!player) return;
        if (player.HasPickable &&
            (player.Pickable.Type == _seedType.GetComponent<Pickable>().Type) &&
            !IsSeeded)
        {
            Seed();
            Destroy(player.Pickable.gameObject);
            player.RemovePickable();
        }
    }

    void OnTriggerStay(Collider collision)
    {
        if (!IsSeeded) return;
        var player = collision.GetComponent<Player>();
        if (player && player.HasPickable &&
           player.Pickable.Type == Pickable.PickableType.WATER_CONTAINER && !_isFullyGrown)
        {
            WateringCan wateringCanScript = player.Pickable.gameObject.GetComponent<WateringCan>();
            
            if (wateringCanScript.GetWaterAmount() >= 0)
            {
                wateringCanScript.UseWater();
                Grow();
            }
        }
    }
}
