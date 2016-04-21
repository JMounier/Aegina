using UnityEngine;
using System.Collections;

public class SuccessDatabase
{
    public static readonly Success Root = new Success(0, TextDatabase.Apple, ItemDatabase.Workbench, new Requirement.Requirements[1] { Requirement.Requirements.Tuto }, 0);
}
