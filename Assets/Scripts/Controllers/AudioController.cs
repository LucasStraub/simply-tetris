using UnityEngine;

public class AudioController : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private Animator _musicAnimator;
    [SerializeField] private AudioSource _effects;
#pragma warning restore 0649

    public void Play(AudioClip clip, float volume)
    {
        if (clip == null)
            return;

        _effects.Stop();
        _effects.clip = clip;
        _effects.volume = volume;
        _effects.Play();
    }

    public void SetMusicActive(bool value)
    {
        _musicAnimator.SetBool("Open", value);
    }
}
