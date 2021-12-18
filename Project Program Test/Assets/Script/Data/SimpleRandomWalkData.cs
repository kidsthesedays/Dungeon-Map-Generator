using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleRandomWalkParameters_",menuName= "PCG/SimpleRandomWalkData")]//Defines File name that is needed to save parameters.
public class SimpleRandomWalkData : ScriptableObject //Inherites from ScibtableObject to.
{
    public int iterations = 10, walkLength = 10; //Base values from a new saved generation
    public bool startRandomlyEachIteration = true; 

}
