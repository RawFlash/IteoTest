using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimpleSliderController : MonoBehaviour
{
    public static SimpleSliderController Instance;

    public List<SliderInfoSimple> sliderInfos;
    public Transform previewParent;

    public GameObject previewPrefab;

    public Image slideImage;
    public TMP_Text title;
    public TMP_Text description;

    private List<SimpleSliderPreviewController> sliderPreviewControllers;

    private void Start()
    {
        Instance = this;

        SetSimplePreview();
        SetSimpleFirst();
    }

    private void SetSimplePreview()
    {
        sliderPreviewControllers = new();

        foreach (var sliderInfo in sliderInfos)
        {
            var pr = Instantiate(previewPrefab, previewParent);
            pr.GetComponent<SimpleSliderPreviewController>().Init(sliderInfo);

            sliderPreviewControllers.Add(pr.GetComponent<SimpleSliderPreviewController>());
        }
    }

    private void SetSimpleFirst()
    {
        sliderPreviewControllers[0].Select();
    }

    public void SetSlide(SimpleSliderPreviewController sliderPreviewController)
    {
        slideImage.sprite = sliderPreviewController.sliderInfo.image;
        title.text = sliderPreviewController.sliderInfo.title;
        description.text = sliderPreviewController.sliderInfo.description;

        DeselectAll();

        sliderPreviewController.SetSelectStatus(true);
    }

    public void DeselectAll()
    {
        foreach (var preview in sliderPreviewControllers)
        {
            preview.SetSelectStatus(false);
        }
    }

    public void LeftSimple()
    {
        int currentIndex = GetSelectedIndex();

        int newIndex;

        if (currentIndex == 0)
        {
            newIndex = sliderPreviewControllers.Count - 1;
        }
        else
        {
            newIndex = currentIndex - 1;
        }

        SetSlide(sliderPreviewControllers[newIndex]);
    }

    public void RightSimple()
    {
        int currentIndex = GetSelectedIndex();

        int newIndex;

        if (currentIndex == sliderPreviewControllers.Count-1)
        {
            newIndex = 0;
        }
        else
        {
            newIndex = currentIndex + 1;
        }

        SetSlide(sliderPreviewControllers[newIndex]);
    }

    private int GetSelectedIndex() => sliderPreviewControllers.FindIndex(pr => pr.select == true);
}
