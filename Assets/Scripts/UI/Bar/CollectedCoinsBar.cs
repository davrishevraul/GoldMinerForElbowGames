using TMPro;
using UnityEngine;

public class CollectedCoinsBar : MonoBehaviour
{
	[Header("Text")]
	[SerializeField] private TMP_Text _coinsCount;

	private void OnEnable()
	{
		MinerPlatform.CoinsAmountChanged += UpdateText;

		UpdateText(MinerPlatform.CollectedCoinsCount);
	}

	private void OnDisable()
	{
		MinerPlatform.CoinsAmountChanged -= UpdateText;
	}

	private void UpdateText(int coinsCount)
	{
		_coinsCount.text = $"{coinsCount}";
	}
}