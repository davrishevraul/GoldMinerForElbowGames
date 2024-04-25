using UnityEngine;
using UnityEngine.UI;

public class SettingsWindow : MonoBehaviour
{
	[Header("Sliders")]
	[SerializeField] private Slider _sliderMusic;
	[SerializeField] private Slider _sliderSound;

	private void Start()
	{
		UpdateSlidersValue();
		AddSlidersListener();
	}

	private void UpdateSlidersValue()
	{
		_sliderMusic.value = SoundSystem.Instance.BackgroundMusicVolume;
		_sliderSound.value = SoundSystem.Instance.SoundFXVolume;
	}

	private void AddSlidersListener()
	{
		_sliderMusic.onValueChanged.AddListener(SoundSystem.Instance.SetBackgroundMusicVolume);
		_sliderSound.onValueChanged.AddListener(SoundSystem.Instance.SetSoundFxVolume);
	}
}
