using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleIsKinematicInChildren : MonoBehaviour
{
	//// Przycisk w inpektorze, który odpala fukncję ToggleKinematic
	//[InspectorButton("ToggleKinematic", ButtonWidth = 140)] public bool Toggle_Kinematic;

	// Tablica do przechowywania wszytskich komponentów
	// Rigidbody w dzieciach, żeby można wyłączyć isKinematic
	private Rigidbody[] rigidBodies;
	
    /// <summary>
	/// Zapełnił tablicę komponentami i wyłącz isKinematic
	/// </summary>
    void Start ()
	{
		rigidBodies = GetComponentsInChildren<Rigidbody>();
		ToggleKinematic();
	}
	
	/// <summary>
	/// Wyłącza isKinematic w każdym dziecku
	/// </summary>
	public void ToggleKinematic()
	{
		foreach (Rigidbody rigidBody in rigidBodies)
		{
			if (rigidBody)
			{
				rigidBody.isKinematic = !rigidBody.isKinematic;
			}
		}
	}
}
