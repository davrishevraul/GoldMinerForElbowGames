using UnityEngine;

public class SoundSystem : MonoBehaviour
{
	[Header("Background"), Space(5)]
	[SerializeField] private AudioSource _backgroundMusic;

	[Header("Sound FX"), Space(5)]
	[SerializeField] private AudioSource _collectSoundFX;
	[SerializeField] private AudioSource _crashSoundFX;

	public float BackgroundMusicVolume => _backgroundMusic.volume;

	public float SoundFXVolume { get; private set; }

	public static SoundSystem Instance { get; private set; }

	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
	}

	private void Start()
	{
		LoadSettings();
	}

	private void OnDestroy()
	{
		SaveSettingsToPlayerPrefs();
	}

	public void PlayCollectSound()
	{
		_collectSoundFX.volume = SoundFXVolume;
		_collectSoundFX.Play();
	}

	public void PlayCrashSound()
	{
		_crashSoundFX.volume = SoundFXVolume;
		_crashSoundFX.Play();
	}

	public void SetBackgroundMusicVolume(float volume)
	{
		_backgroundMusic.volume = volume;
	}

	public void SetSoundFxVolume(float volume)
	{
		SoundFXVolume = volume;
	}

	private void LoadSettings()
	{
		SoundFXVolume = PlayerPrefs.GetFloat(nameof(SoundFXVolume), 1.0f);
		_backgroundMusic.volume = PlayerPrefs.GetFloat(nameof(BackgroundMusicVolume), BackgroundMusicVolume);
	}

	private void SaveSettingsToPlayerPrefs()
	{
		PlayerPrefs.SetFloat(nameof(SoundFXVolume), SoundFXVolume);
		PlayerPrefs.SetFloat(nameof(BackgroundMusicVolume), BackgroundMusicVolume);

		PlayerPrefs.Save();
	}
}