using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Przeciwnik_Poscig_Advanced : MonoBehaviour
{
	// Odniesienie do komponentu NavMeshAgent
	// naszego przeciwnika - jest to szybsze niż
	// każdorazowe używanie fukncji GetComponent<NavMeshAgent>();
	private NavMeshAgent mojNavMeshAgent;

	[SerializeField] private float promienWyszukiwania;		// w jakim promieniu "widzi" przeciwnik

	private Collider[] znalezioneObiekty;
	public bool CzyNavMeshAktywny = true;
	

	void Start ()
	{
		mojNavMeshAgent = GetComponent<NavMeshAgent>();
	}
	
	void Update ()
	{
		WyszukajCel();
	}

	private void WyszukajCel()
	{
		if (!CzyNavMeshAktywny) return;		// jeśli NavMeshAgent nieaktywny, wychodzimy

		mojNavMeshAgent.SetDestination(transform.position);
		znalezioneObiekty = Physics.OverlapSphere(transform.position, promienWyszukiwania);

		foreach (Collider obiekt in znalezioneObiekty)
		{
			
			if (obiekt.CompareTag("Player"))
			{
				print("Gracz w zasięgu");
				if (SprawdzWidocznosc(obiekt.transform.position))
				{
					mojNavMeshAgent.SetDestination(obiekt.transform.position);
				}			
			}
		}
	}

	private bool SprawdzWidocznosc(Vector3 playerPosition)
	{
		Vector3 directionToPLayer = playerPosition - transform.position;
		RaycastHit cel;

		if (Physics.Raycast(transform.position, directionToPLayer, out cel, promienWyszukiwania))
		{
			print("Widzę " + cel.transform.name);
			if (cel.transform.CompareTag("Player"))
			{
				return true;
			}
		}

		return false;
	}
}
