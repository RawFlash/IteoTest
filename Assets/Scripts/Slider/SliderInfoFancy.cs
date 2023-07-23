using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SliderInfoFancy
{
    public Sprite Image { get; }
    public string Title { get; }
    public string Description { get; }

    public SliderInfoFancy(Sprite image, string title, string description)
    {
        Image = image;
        Title = title;
        Description = description;
    }

    public SliderInfoFancy(FancySliderInfoImport rawInfo, Sprite image)
    {
        Image = image;
        Title = rawInfo.title;
        Description = rawInfo.description;
    }
}

public class FancySliderInfoImport
{
    public string url;
    public string title;
    public string description;
}
