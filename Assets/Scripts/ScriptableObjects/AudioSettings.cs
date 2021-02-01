using UnityEngine;

[CreateAssetMenu(fileName = "AudioSettings", menuName = "ScriptableObjects/AudioSettings", order = 1)]
public class AudioSettings : ScriptableObject
{
#pragma warning disable 0649
    [Header("Menu")]
    [SerializeField] private AudioClip _menuMusic;
    public AudioClip MenuMusic => _menuMusic;

    [Header("Game")]
    [SerializeField] private AudioClip _gameMusic;
    public AudioClip GameMusic => _gameMusic;

    [SerializeField] private AudioClip _moveSound;
    public AudioClip MoveSound => _moveSound;
    [SerializeField] private float _moveSoundVolume = 1;
    public float MoveSoundVolume => _moveSoundVolume;

    [SerializeField] private AudioClip _rotateSound;
    public AudioClip RotateSound => _rotateSound;
    [SerializeField] private float _rotateSoundVoulme = 1;
    public float RotateSoundVolume => _rotateSoundVoulme;

    [SerializeField] private AudioClip _snapSound;
    public AudioClip SnapSound => _snapSound;
    [SerializeField] private float _snapSoundVoulme = 1;
    public float SnapSoundVolume => _snapSoundVoulme;

    [SerializeField] private AudioClip _cleanRowSound;
    public AudioClip CleanRowSound => _cleanRowSound;
    [SerializeField] private float _cleanRowSoundVoulme = 1;
    public float CleanRowSoundVolume => _cleanRowSoundVoulme;

    [SerializeField] private AudioClip _gameOverSound;
    public AudioClip GameOverSound => _gameOverSound;
    [SerializeField] private float _gameOverSoundVoulme = 1;
    public float GameOverSoundVolume => _gameOverSoundVoulme;
#pragma warning restore 0649
}
