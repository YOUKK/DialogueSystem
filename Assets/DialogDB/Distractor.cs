using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class Distractor : ScriptableObject
{
	public List<DistractorDB> Sheet1; // Replace 'EntityType' to an actual type that is serializable.
}
