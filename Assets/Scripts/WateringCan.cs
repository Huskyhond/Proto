using UnityEngine;

public class WateringCan : MonoBehaviour
{
    [SerializeField] private RectTransform BackgroundBar;
    [SerializeField] private RectTransform WaterBar;

    [SerializeField] private int MaxWaterAmount = 147; // Currently uses 49 water to fully grow a mushroom
    private int _waterAmount;

    private void Start()
    {
        _waterAmount = MaxWaterAmount;
        BackgroundBar.sizeDelta = new Vector3(_waterAmount, BackgroundBar.sizeDelta.y);
        WaterBar.sizeDelta = new Vector2(_waterAmount, WaterBar.sizeDelta.y);
    }

    public void FillWateringCan()
    {
        _waterAmount = MaxWaterAmount;
        WaterBar.sizeDelta = new Vector2(_waterAmount, WaterBar.sizeDelta.y);
    }

    public int GetWaterAmount()
    {
        return _waterAmount;
    }

    public void UseWater()
    {
        _waterAmount--;
        WaterBar.sizeDelta = new Vector2(_waterAmount, WaterBar.sizeDelta.y);
    }
}
