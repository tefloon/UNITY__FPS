using UnityEngine;

public class Granat_Wybuch : MonoBehaviour
{
	[SerializeField] private float srednicaWybuchu;
	[SerializeField] private float silaWybuchu;
	[SerializeField] private LayerMask warstwyWybuch;
	[SerializeField] private GameObject efektWybuchu;

	private Collider[] trafioneObiekty;

	private void OnCollisionEnter(Collision col)
	{
		Vector3 punktWybuchu = col.contacts[0].point;

		var go = Instantiate(efektWybuchu, punktWybuchu, Quaternion.identity);
		go.transform.rotation = Quaternion.LookRotation(col.contacts[0].normal.normalized);

		WykonajWybuch(punktWybuchu);
		Destroy(gameObject);
		Destroy(go, 2f);
	}


	private void WykonajWybuch(Vector3 punktWybuchu)
	{
		trafioneObiekty = Physics.OverlapSphere(punktWybuchu, srednicaWybuchu, warstwyWybuch);

		foreach (Collider hitCol in trafioneObiekty)
		{
			if (hitCol.GetComponent<Rigidbody>() != null)
			{
				hitCol.GetComponent<Rigidbody>().AddExplosionForce
				(
					silaWybuchu,
					punktWybuchu,
					srednicaWybuchu, 1,
					ForceMode.Impulse
				);
			}
		}
	}
}