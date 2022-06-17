using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicLooper : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] backgroundMusicClips;
    private int currentMusicClip = 0;

    private void Start()
    {
        GetComponent<AudioSource>().clip = backgroundMusicClips[currentMusicClip];
        GetComponent<AudioSource>().Play();
    }
    public void ChangeMusicClip()
    {
        currentMusicClip++;
        GetComponent<AudioSource>().clip = backgroundMusicClips[currentMusicClip];
        GetComponent<AudioSource>().Play();
    }
}
