using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gracz_Strzal : MonoBehaviour
{
	[Header("Efekty strzału")]
	[SerializeField] private GameObject efektTrafienia;
	[SerializeField] private AudioClip dzwiekStrzalu;

	[Header("Opcje strzału")]
	[SerializeField] private Transform miejsceStrzalu;
	[Range(0.01f, 5f)][SerializeField] private float odstepStrzalow;


	[Header("Opcje granatów")]
	[SerializeField] private Transform miejsceGranatu;
	[SerializeField] private GameObject granatPref;
	[SerializeField] private float silaRzutu;
	[Range(1f, 15f)] [SerializeField] private float odstepGranatow;

	private float nastepnyStrzal = 0;
	private float nastepnyGranat = 0;
	private RaycastHit cel;
	private AudioSource audioSource;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		SprawdzWejscie();
	}

	void SprawdzWejscie()
	{
		if (Input.GetButton("Fire1") && Time.time >= nastepnyStrzal)
		{
			print("Strzelam!");
			StrzalRaycast();
			nastepnyStrzal = Time.time + odstepStrzalow;
		}

		if (Input.GetKeyDown(KeyCode.G) && Time.time >= nastepnyGranat)
		{
			print("Rzucam!");
			RzutGranatem();
			nastepnyGranat = Time.time + odstepGranatow;
		}
	}

	void StrzalRaycast()
	{
		if (Physics.Raycast(miejsceStrzalu.position, miejsceStrzalu.forward, out cel))
		{
			Debug.DrawRay(miejsceStrzalu.position, miejsceStrzalu.forward * 50f, Color.green, 2f);

			var go = Instantiate(efektTrafienia, cel.point, Quaternion.identity);
			go.transform.rotation = Quaternion.LookRotation(cel.normal.normalized);

			audioSource.PlayOneShot(dzwiekStrzalu);

			print(cel.transform.name);
		}
	}

	void RzutGranatem()
	{
		var go = Instantiate(granatPref, miejsceGranatu.position, Quaternion.identity);
		var rb = go.GetComponent<Rigidbody>();
		rb.AddForce(miejsceGranatu.forward * silaRzutu, ForceMode.Impulse);
	}
}
