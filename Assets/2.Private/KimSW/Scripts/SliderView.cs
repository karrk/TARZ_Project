using UnityEngine;
using UnityEngine.UI;

public class SliderView : MonoBehaviour
{
    [SerializeField] protected Slider slider;
 
    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();

    }


    public virtual void SetSlider(float value)
    {
        slider.value = value;
    }
    public virtual void SetSliderMax(float value)
    {
        slider.maxValue = value;

    }
}
