using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicLooper : MonoBehaviour
{
    [SerializeField]
    private OnPuzzleProgression[] backgroundMusicClips;
    private int currentMusicClip = 0;

    private void Start()
    {
        GetComponent<AudioSource>().clip = backgroundMusicClips[currentMusicClip].newBackgroundMusic;
        GetComponent<AudioSource>().Play();
    }
    public void ChangeMusicClip()
    {
        currentMusicClip++;
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Stop();

        if (backgroundMusicClips[currentMusicClip].singleSoundClip != null)
        {
            audioSource.PlayOneShot(backgroundMusicClips[currentMusicClip].singleSoundClip);
            Invoke(nameof(PlayNextBackgroundClip), backgroundMusicClips[currentMusicClip].singleSoundClip.length);
        }
        else
        {
            audioSource.clip = backgroundMusicClips[currentMusicClip].newBackgroundMusic;
            print("Play: " + backgroundMusicClips[currentMusicClip].newBackgroundMusic.name);
            audioSource.Play();
        }
    }
    private void PlayNextBackgroundClip()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = backgroundMusicClips[currentMusicClip].newBackgroundMusic;
        audioSource.Play();
    }
}

[System.Serializable]
public class OnPuzzleProgression
{
    public AudioClip singleSoundClip;
    public AudioClip newBackgroundMusic;
}
