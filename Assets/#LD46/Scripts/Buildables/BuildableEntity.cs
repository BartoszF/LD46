using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "BuildableEntity", menuName = "LD46/BuildableEnity", order = 1)]
public class BuildableEntity : ScriptableObject
{
   public int cost;

   public GameObject prefab;

   public BuildingModeEnum buildingMode;

   public List<OrientationData> orientationDataArr = new List<OrientationData>(4);

}

[Serializable]
    public struct OrientationData {
        public OrientationEnum orientation;
        public Sprite spriteToRender;

        public RuntimeAnimatorController controller;
    }