using FancyScrollView;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FancySliderPreviewController : FancyCell<SliderInfoFancy>
{
    public Image image;

    [HideInInspector]
    public SliderInfoFancy data;

    public override void UpdateContent(SliderInfoFancy itemData)
    {
        image.sprite = itemData.Image;
        data = itemData;
    }

    public override void UpdatePosition(float position)
    {
        
    }

    public void Select()
    {
        FancySliderController.Instance.Select(this);
    }
}
