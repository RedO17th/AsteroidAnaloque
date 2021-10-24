using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public SoundSystem.SoundType type;
    public AudioClip audioClip;
    public AudioSource audioSource;
    [Range(0f, 1f)]
    public float volume;
    public float pitch;
    public bool isLoop;
    public float pitchRange;
    public bool playOnAwake;
}

public class SoundSystem : BaseSystem
{
    public enum SoundType { None = -1, Shot, Accelerate, BigAsteroid, MiddleAsteroid, SmallAsteroid }

    [SerializeField] private List<Sound> sounds;

    private static SoundSystem _instance;

    private GameController _gameController;

    private void Awake()
    {
        if (_instance != null) Destroy(_instance.gameObject);
        _instance = this;
    }

    protected override void InitializeData()
    {
        _gameController = _systemInitializer.GameController;
        ScreateSoundSource();
    }

    public override void AdditionalInitialize()
    {
        _gameController.OnStartGameEvent += StopAllSound;
        _gameController.OnPauseGameEvent += StopAllSound;
    }

    private void ScreateSoundSource()
    {
        foreach (Sound sound in sounds)
        {
            var newas = gameObject.AddComponent<AudioSource>();
            newas.clip = sound.audioClip;
            newas.loop = sound.isLoop;
            newas.playOnAwake = sound.playOnAwake;
            newas.pitch = sound.pitch;
            newas.volume = sound.volume;
            sound.audioSource = newas;
        }
    }

    public static void PlaySound(SoundType type)
    {
        var sound = _instance.sounds.SingleOrDefault(s => s.type == type);
        if (sound == null) return;
        sound.audioSource.Play();
    }

    public static void StopSound(SoundType type)
    {
        var sound = _instance.sounds.SingleOrDefault(s => s.type == type);
        if (sound == null) return;
        sound.audioSource.Stop();
    }

    private static void StopAllSound()
    {
        foreach (var sound in _instance.sounds)
        {
            if (sound == null) continue;
            sound.audioSource.Stop();
        }
    }

    private void OnDisable()
    {
        _gameController.OnStartGameEvent -= StopAllSound;
        _gameController.OnPauseGameEvent -= StopAllSound;
    }
}
