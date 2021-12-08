using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;

	private void Update()
    {
		if (!ps.IsAlive(true))
		{
			CObjectPool.instance.PoolObject(gameObject);
		}
	}
}
