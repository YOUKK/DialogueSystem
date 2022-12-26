using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class MyDialog : ScriptableObject
{
	public List<MyDialogDB> Sheet1; // Replace 'EntityType' to an actual type that is serializable.
}
