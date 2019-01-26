using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int MaxSize;
    public Vector2 StartPos;
    public Vector2 MoveDis;
    public List<Passenge> Passenges = new List<Passenge>();
    public void AddPassenge(Passenge passenge)
    {
        if(Passenges.Count >= MaxSize)
        {
            Passenges.Remove(Passenges[0]);
            Destroy(Passenges[0]);
        }
        for(int i=0;i< Passenges.Count; i++)
        {
            Passenges[i].transform.position += (Vector3)MoveDis;
        }

        passenge.transform.position = StartPos;
        Passenges.Add(passenge);
    }
    public void RemovePassenge(Passenge passenge)
    {
        Passenges.Remove(passenge);
    }
}
