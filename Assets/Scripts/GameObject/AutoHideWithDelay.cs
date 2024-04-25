using System.Collections;
using UnityEngine;

public class AutoHideWithDelay : MonoBehaviour
{
	[SerializeField, Min(0.01f)] private float _delay = 2.0f;

	private void OnEnable()
	{
		StartCoroutine(Hide());
	}

	private IEnumerator Hide()
	{
		yield return new WaitForSeconds(_delay);

		gameObject.SetActive(false);
	}
}
