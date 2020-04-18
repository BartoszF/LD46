using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildableEntity", menuName = "LD46/BuildableEnity", order = 1)]
public class BuildableEntity : ScriptableObject
{
   public int cost;

   public GameObject prefab;

   public BuildingModeEnum buildingMode;

}
