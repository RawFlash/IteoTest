using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MainController : MonoBehaviour, IPointerClickHandler
{
    public static MainController Instance;

    public VideoPlayerController videoPlayerController;

    public GameObject bar;

    public float delayHideBar;

    public GameObject ErrorPrefab;

    private List<string> clips;

    private bool isShowBar;

    private Vector3 showPosBar;

    private Vector3 hidePosBar;

    private float leftTimeHide;

    private int currentClipIndex;


    private void Start()
    {
        Instance = this;

        DOTween.Init();
        InitPosBar();

        videoPlayerController.InitPlayer();

        LoadVideo();
    }

    private void InitPosBar()
    {
        hidePosBar = bar.transform.position;
        showPosBar = bar.transform.TransformPoint(new Vector3(bar.transform.localPosition.x, 100f, bar.transform.localPosition.z));
    }

    private void LoadVideo()
    {
        try
        {
            clips = new(File.ReadLines(Application.streamingAssetsPath + "\\playlist.txt"));
        }
        catch(Exception e)
        {
            ShowError(TypeError.MisPlaylist, e.Message);
            return;
        }
        

        for(int i = 0; i < clips.Count; i++)
        {
            clips[i] = Application.streamingAssetsPath + "\\" + clips[i];
        }

        currentClipIndex = 0;

        videoPlayerController.LoadVideo(clips[0]);
    }

    public void BackVideo()
    {
        videoPlayerController.LoadVideo(clips[NextVideoIndex(true)]);
    }

    public void NextVideo()
    {
        videoPlayerController.LoadVideo(clips[NextVideoIndex()]);
    }

    private int NextVideoIndex(bool isBack = false)
    {
        if (!isBack)
        {
            currentClipIndex++;

            if (currentClipIndex >= clips.Count)
            {
                currentClipIndex = 0;
            }
        }
        else
        {
            currentClipIndex--;

            if (currentClipIndex <= -1)
            {
                currentClipIndex = clips.Count-1;
            }
        }
        return currentClipIndex;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isShowBar)
        {
            bar.transform.DOMove(showPosBar, 0.5f);

            isShowBar = true;
            leftTimeHide = 0.5f + delayHideBar;

            StartCoroutine(DelayHideBar(
                () =>
                {
                    bar.transform.DOMove(hidePosBar, 0.5f);
                    isShowBar = false;
                }
                ));
        }
        else
        {
            UpdateTimeDelay();
        }
    }

    public void UpdateTimeDelay()
    {
        leftTimeHide = delayHideBar;
    }

    
    public IEnumerator DelayHideBar(UnityAction hide)
    {
        while(leftTimeHide > 0f)
        {
            yield return new WaitForSeconds(1);
            leftTimeHide--;
        }

        hide?.Invoke();
    }

    public void ShowError(TypeError type, string exception)
    {
        GameObject error =  Instantiate(ErrorPrefab, transform);

        string descr = "����������� ������";

        switch(type)
        {
            case TypeError.MisPlaylist:
                descr = "������ � ������ ���������: " + Application.streamingAssetsPath + "\\playlist.txt" + "\n���� �� ������ ��� �������� ������";
                break;

            case TypeError.MisClip:
                descr = "������ � ������ �����: " + Application.streamingAssetsPath + clips[currentClipIndex] + "\n���� �� ������ ��� �� ��������������";
                break;
        }

        error.GetComponent<ErrorController>().Init(descr, exception);
    }
}

public enum TypeError
{
    MisPlaylist,
    MisClip
}