using UnityEngine;
using UnityEngine.UI;

public class SimpleSliderPreviewController: MonoBehaviour
{
    [HideInInspector]
    public SliderInfoSimple sliderInfo;

    public Image image;

    [HideInInspector]
    public bool select;

    public Outline outline;


    public void Init(SliderInfoSimple sliderInfo)
    {
        this.sliderInfo = sliderInfo;
        image.sprite = sliderInfo.image;
    }

    public void Select()
    {
        SimpleSliderController.Instance.SetSlide(this);
    }

    public void SetSelectStatus(bool status)
    {
        select = status;
        outline.enabled = status;
    }
}
