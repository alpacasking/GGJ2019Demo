using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int SlotSize;
    public Vector2 StartPos;
    public Vector2 MoveDis;
    public List<Passenge> Passenges = new List<Passenge>();
    public List<Passenge> PassengePrefabs;
    public int PassengeCount;
    public int SpawnedPassengeCount;
    public int PassengeLeftCount;
    void Start()
    {
        for(int i=0;i< SlotSize; i++)
        {
            var passenge = Instantiate(PassengePrefabs[Random.Range(0, PassengePrefabs.Count)]);
            Vector3 pos = StartPos + MoveDis * i;
            pos.z = -1;
            passenge.transform.position = pos;
            Passenges.Add(passenge);
            SpawnedPassengeCount++;
        }
        PassengeLeftCount = 0;
    }
    public void AddPassenge(Passenge passenge)
    {
        var tempPassenges = Passenges;
        Passenges = new List<Passenge>();
        Passenges.Add(passenge);
        Passenges.AddRange(tempPassenges);
        for (int i = 0; i < Passenges.Count; i++)
        {
            Vector3 pos = StartPos + MoveDis * i;
            pos.z = -1;
            Passenges[i].transform.position = pos;
        }
        PassengeLeftCount--;
    }
    public void SpawnPassenge()
    {
        var passenge = Instantiate(PassengePrefabs[Random.Range(0, PassengePrefabs.Count)]);
        Passenges.Add(passenge);
        SpawnedPassengeCount++;
        for (int i = 0; i < Passenges.Count; i++)
        {
            Vector3 pos = StartPos + MoveDis * i;
            pos.z = -1;
            Passenges[i].transform.position = pos;
        }
    }
    public void RemovePassenge(Passenge passenge)
    {
        Passenges.Remove(passenge);
        if(Passenges.Count < SlotSize && SpawnedPassengeCount < PassengeCount)
        {
            SpawnPassenge();
        }
        for (int i = 0; i < Passenges.Count; i++)
        {
            Vector3 pos = StartPos + MoveDis * i;
            pos.z = -1;
            Passenges[i].transform.position = pos;
        }
        PassengeLeftCount++;
    }

    public bool IsPassengeAllLeft()
    {
        return PassengeLeftCount == PassengeCount;
    }
}
