using UnityEngine;
using System.Collections;

public class SetParticleSortingLayer : MonoBehaviour {

	public string sortingLayerName;
	public int sortingOrder;

	void Start () {
	
		particleSystem.renderer.sortingLayerName = sortingLayerName;
		particleSystem.renderer.sortingOrder = sortingOrder;
	}
}
