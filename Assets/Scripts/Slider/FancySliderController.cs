using FancyScrollView;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FancySliderController : MonoBehaviour
{
    public static FancySliderController Instance;

    [SerializeField] 
    FancyScrollController myScrollView;

    public List<SliderInfoFancy> sliders;

    public Image image;
    public TMP_Text title;
    public TMP_Text description;

    void Start()
    {
        Instance = this;

        sliders = GetLoadSliders();

        myScrollView.UpdateData(sliders);

        image.enabled = false;
        title.text = "";
        description.text = "";
    }

    private List<SliderInfoFancy> GetLoadSliders()
    {
        List<SliderInfoFancy> ret = new();

        DirectoryInfo dir = new(Application.streamingAssetsPath + "\\descriptions\\");
        int count = dir.GetFiles().Length;

        count /= 2;

        for(int i = 0; i < count; i++)
        {
            string json = File.ReadAllText(Application.streamingAssetsPath + "\\descriptions\\" + (i+1).ToString() + ".json");

            var tempInfo = JsonUtility.FromJson<FancySliderInfoImport>(json);

            Sprite tempImage = LoadNewSprite(Application.streamingAssetsPath + tempInfo.url);

            ret.Add(new SliderInfoFancy(tempInfo, tempImage));
        }

        return ret;
    }

    public void Select(FancySliderPreviewController select)
    {
        image.enabled = true;
        image.sprite = select.data.Image;
        title.text = select.data.Title;
        description.text = select.data.Description;
    }

    public Sprite LoadNewSprite(string FilePath, float PixelsPerUnit = 100.0f)
    {
        Texture2D SpriteTexture = LoadTexture(FilePath);
        Sprite NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0, 0), PixelsPerUnit);

        return NewSprite;
    }

    public Texture2D LoadTexture(string FilePath)
    {
        Texture2D Tex2D;
        byte[] FileData;

        if (File.Exists(FilePath))
        {
            FileData = File.ReadAllBytes(FilePath);
            Tex2D = new Texture2D(2, 2);           
            if (Tex2D.LoadImage(FileData))           
                return Tex2D;                 
        }
        return null;   
    }
}
