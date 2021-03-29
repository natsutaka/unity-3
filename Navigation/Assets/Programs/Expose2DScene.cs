using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Expose Scene Property/3D")]
public class Expose2DScene : ScriptableObject
{
	public Transform player { get; set; }
}