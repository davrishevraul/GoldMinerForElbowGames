using UnityEngine;

public class CollectableCoin : MonoBehaviour, ICollectable
{
	public void Collect()
	{
		SoundSystem.Instance.PlayCollectSound();
		gameObject.SetActive(false);
	}
}