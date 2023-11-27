using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour {

	public static ItemCollector Instance 
	{
		get
		{
			if(instance == null)
			{
				if(FindObjectOfType<ItemCollector>() == null)
				{
					var singleton = new GameObject("Coin Collector");

					instance = singleton.AddComponent<ItemCollector>();
				}

				return instance;
			}

			return instance;
		}
	}

	private  static ItemCollector instance;
	private int allCoinAmount;
	private int coinAmount = 0;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
	}

	void Start()
	{
		allCoinAmount = GameObject.FindObjectsOfType<Item>().Length;
	}

	void OnGUI()
	{
		GUI.TextArea(new Rect(0, 0, 180, 20), "Ключ карт собрано: " + coinAmount.ToString() + " /  " + allCoinAmount.ToString());
	}

	public void AddCoin()
	{
		coinAmount++;
	}

	public void Init()
	{
		
	}
}
