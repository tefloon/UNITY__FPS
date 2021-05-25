using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class SpawnerScripts : MonoBehaviour
{
	public Transform[] MiejscaSpawnu;
	public Fala[] Fale;

	[Serializable]
	public class Fala
	{
		public Grupa[] Grupy;
		public float CzasFali; 
	}

	[Serializable]
	public class Grupa
	{
		public GameObject PrzeciwnikPrefab;
		public int LiczbaPrzeciwnikow;
	}

	int numerFali;

	void SpawnujFale()
	{
		int indeksSpawnu = UnityEngine.Random.Range(0, MiejscaSpawnu.Length);   // Losujemy miejsce

		for (int g = 0; g < Fale[numerFali].Grupy.Length; g++)
		{
			for (int p = 0; p < Fale[numerFali].Grupy[g].LiczbaPrzeciwnikow; p++)
			{
				Instantiate(Fale[numerFali].Grupy[g].PrzeciwnikPrefab,  // Pojawiamy przeciwnika z obecnej Fali i wybranej grupy
						MiejscaSpawnu[indeksSpawnu].position,
						Quaternion.identity
						);
			}
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		SpawnujFale();
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
