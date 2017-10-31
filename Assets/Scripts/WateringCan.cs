using UnityEngine;

public class WateringCan : MonoBehaviour
{
    [SerializeField] private int MaxWaterAmount = 147; // Currently uses 49 water to fully grow a mushroom
    private int _waterAmount;

    private void Start()
    {
        _waterAmount = MaxWaterAmount;
    }

    public void FillWateringCan()
    {
        _waterAmount = MaxWaterAmount;
    }

    public int GetWaterAmount()
    {
        return _waterAmount;
    }

    public void UseWater()
    {
        _waterAmount--;
    }
}
