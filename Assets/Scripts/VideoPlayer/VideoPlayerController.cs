using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class VideoPlayerController : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    public Image playPauseImage;

    public Sprite pauseSprite;
    private Sprite playSprite;

    

    public void InitPlayer()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        playSprite = playPauseImage.sprite;
        videoPlayer.errorReceived += (videoPlayer ,e) => MainController.Instance.ShowError(TypeError.MisClip, e);
    }

    public void LoadVideo(string path)
    {
        videoPlayer.url = path;
        videoPlayer.Play();

        playPauseImage.sprite = pauseSprite;

        videoPlayer.loopPointReached += (_) => MainController.Instance.NextVideo();
    }

    public void Next()
    {
        MainController.Instance.UpdateTimeDelay();
        MainController.Instance.NextVideo();
    }

    public void Back()
    {
        MainController.Instance.UpdateTimeDelay();
        MainController.Instance.BackVideo();
    }

    public void Mute(bool mute)
    {
        MainController.Instance.UpdateTimeDelay();
        videoPlayer.SetDirectAudioMute(0, mute);
    }

    public void PlayPause()
    {
        MainController.Instance.UpdateTimeDelay();

        if (playPauseImage.sprite == playSprite)
        {
            playPauseImage.sprite = pauseSprite;
            videoPlayer.Play();
        }
        else
        {
            playPauseImage.sprite = playSprite;
            videoPlayer.Pause();
        }
    }

    public void Reset()
    {
        MainController.Instance.UpdateTimeDelay();

        videoPlayer.Stop();
        videoPlayer.Play();

        playPauseImage.sprite = pauseSprite;
    }
}
