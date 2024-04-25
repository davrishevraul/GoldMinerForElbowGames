using UnityEngine;

public class GameMenu : MonoBehaviour
{
	[Header("Pop Ups")]
	[SerializeField] private GameObject _popUpPause;
	[SerializeField] private GameObject _popUpGameOver;

	[Header("Main Menu")]
	[SerializeField] private GameObject _mainMenu;

	[Header("Game")]
	[SerializeField] private GameObject _gameplayObject;
	[SerializeField] private MinerPlatform _platform;
	[SerializeField] private MineZonesSpawner _spawner;

	private void OnEnable()
	{
		_platform.CharacterFellOut += OnGameOver;
	}

	public void Pause()
	{
		_popUpPause.SetActive(true);

		Time.timeScale = 0;
	}

	public void UnPause()
	{
		_popUpPause.SetActive(false);

		Time.timeScale = 1;
	}

	public void GoToMainMenu()
	{
		_gameplayObject.gameObject.SetActive(false);
		_mainMenu.gameObject.SetActive(true);
		gameObject.SetActive(false);

		Time.timeScale = 1.0f;
	}

	public void StartGame()
	{
		_mainMenu.gameObject.SetActive(false);
		_gameplayObject.gameObject.SetActive(true);
		gameObject.SetActive(true);

		RetryGame();
	}

	public void RetryGame()
	{
		_popUpGameOver.SetActive(false);
		_popUpPause.SetActive(false);

		_platform.MoveToInitialState();
		_spawner.StartFromBeginning();

		Time.timeScale = 1.0f;
	}

	private void OnGameOver()
	{
		_popUpGameOver.SetActive(true);

		Time.timeScale = 0;
	}
}