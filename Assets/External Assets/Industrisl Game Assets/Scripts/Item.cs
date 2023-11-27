using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	[SerializeField] private float rotationSpeed = 200;

	void Start()
	{
		ItemCollector.Instance.Init();
	}

	void Update()
	{
		transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
	}
	void OnTriggerEnter(Collider col)
	{
		ItemCollector.Instance.AddCoin();
		Destroy(gameObject);
	}
}
