using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Przeciwnik_Poscig : MonoBehaviour
{
	// Zmienna przechowująca pozycję gracza
	// W wersji bez OverlapSphere
	[SerializeField] private Transform pozycjaGracza;


	// Odniesienie do komponentu NavMeshAgent
	// naszego przeciwnika - jest to szybsze niż
	// każdorazowe używanie fukncji GetComponent<NavMeshAgent>();
	private NavMeshAgent mojNavMeshAgent;
	private Animator mojAnimator;

	[SerializeField] private float promienWyszukiwania;
	private Collider[] znalezioneObiekty;
	public bool CzyNavMeshAktywny = true;
	

	void Start ()
	{
		mojNavMeshAgent = GetComponent<NavMeshAgent>();
		mojAnimator = GetComponent<Animator>();
	}
	
	void Update ()
	{
		//WyszukajCel_Cheat();
		//WyszukajCel();
		WyszukajCelWidoczny();
	}

	// ====================================================================================== //
	// ====================================================================================== //
	// ====================================================================================== //

	/// <summary>
	/// Podążanie za graczem w nieuczciwy sposób 
	/// z podaniem w inspektorze dostępu do jego pozycji
	/// </summary>
	private void WyszukajCel_Cheat()
	{
		if (pozycjaGracza != null)
		{
			mojNavMeshAgent.SetDestination(pozycjaGracza.position);
		}
	}

	// ====================================================================================== //
	// ====================================================================================== //
	// ====================================================================================== //

	/// <summary>
	/// Podążanie za graczem poprzez wyszukanie 
	/// go w zadanej odległości używajhąc OverlapSphere
	/// </summary>
	private void WyszukajCel()
	{
		if (!CzyNavMeshAktywny) return;

		mojNavMeshAgent.SetDestination(transform.position);
		
		znalezioneObiekty = Physics.OverlapSphere(transform.position, promienWyszukiwania);

		foreach (Collider obiekt in znalezioneObiekty)
		{
			if (obiekt.CompareTag("Player"))
			{
				mojNavMeshAgent.SetDestination(obiekt.transform.position);
			}
		}
	}

	// ====================================================================================== //
	// ====================================================================================== //
	// ====================================================================================== //

	/// <summary>
	/// Podązanie za graczem z OverlapSphere 
	/// i sprawdzeniem czy jest w zasięgu wzroku
	/// (czy nie zasłania żadna ściana)
	/// </summary>
	private void WyszukajCelWidoczny()
	{
		if (!CzyNavMeshAktywny) return;							// jeśli NavMeshAgent nieaktywny, wychodzimy
		mojNavMeshAgent.SetDestination(transform.position);     // "zerujemy" destynację. Jeśli nic nie znajdziemy będziemy stali w miejscu
		mojAnimator.SetBool("czySciga", false);

		// rzucamy OverlapSphere by znaleźć pobliskie obiekty
		znalezioneObiekty = Physics.OverlapSphere(transform.position, promienWyszukiwania);

		foreach (Collider obiekt in znalezioneObiekty)
		{
			if (obiekt.CompareTag("Player"))
			{
				print("Gracz w zasięgu");
				if (SprawdzWidocznosc(obiekt.transform.position))
				{
					mojNavMeshAgent.SetDestination(obiekt.transform.position);
					mojAnimator.SetBool("czySciga", true);
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
