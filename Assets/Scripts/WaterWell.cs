using UnityEngine;

public class WaterWell : MonoBehaviour
{
    [SerializeField] private bool ContainsWater = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (ContainsWater == false) return;

        Player playerScript = collision.gameObject.GetComponent<Player>();

        if (playerScript == null) return;

        if (playerScript.Pickable.Type == Pickable.PickableType.WATER_CONTAINER)
        {
            WateringCan wateringCanScript = playerScript.Pickable.gameObject.GetComponent<WateringCan>();
            wateringCanScript.FillWateringCan();
        }
    }
}
