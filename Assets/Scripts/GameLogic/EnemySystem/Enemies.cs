using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemies : MonoBehaviour {

    public List<GameObject> m_Enemies = new List<GameObject>();
    public List<GameObject> m_Bosses = new List<GameObject>();
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject GetRandomBoss()
    {
        int index = Random.Range(0, m_Bosses.Count);
        return (GameObject)GameObject.Instantiate(m_Bosses[index], new Vector3(100, 100, 100), m_Bosses[index].transform.localRotation);
    }
    public GameObject GetRandomEnemy()
    {
        int index = Random.Range(0, m_Enemies.Count);
        return (GameObject)GameObject.Instantiate(m_Enemies[index],new Vector3(100,100,100),m_Enemies[index].transform.localRotation);
    }
    public GameObject[] GetRandomEnemies(int count)
    {
        bool ignoreDouble = false;
        if (count <= 0) return new GameObject[0];
        if (m_Enemies.Count <= count) ignoreDouble = true;
        List<int> indices = new List<int>();
        if (ignoreDouble)
        {
            for(int i = 0; i < count; i++)
            {
                indices.Add(Random.Range(0, m_Enemies.Count));
            }
        }else
        {
            List<int> toChoose = new List<int>();
            for (int i = 0; i < m_Enemies.Count; i++)
            {
                toChoose.Add(i);
            }
            for (int i = 0; i < count; i++)
            {
                int index = Random.Range(0, toChoose.Count);
                indices.Add(toChoose[index]);
                toChoose.RemoveAt(index);
            }
        }

        List<GameObject> objects = new List<GameObject>();

        for (int i = 0; i < indices.Count; i++)
        {
            objects.Add((GameObject)GameObject.Instantiate(m_Enemies[indices[i]], new Vector3(100, 100, 100), m_Enemies[indices[i]].transform.localRotation));
        }

        return objects.ToArray();
    }
    public GameObject[] GetRandomBosses(int count)
    {
        bool ignoreDouble = false;
        if (count <= 0) return new GameObject[0];
        if (m_Bosses.Count <= count) ignoreDouble = true;
        List<int> indices = new List<int>();
        if (ignoreDouble)
        {
            for (int i = 0; i < count; i++)
            {
                indices.Add(Random.Range(0, m_Bosses.Count));
            }
        }
        else
        {
            List<int> toChoose = new List<int>();
            for (int i = 0; i < m_Bosses.Count; i++)
            {
                toChoose.Add(i);
            }
            for (int i = 0; i < count; i++)
            {
                int index = Random.Range(0, toChoose.Count);
                indices.Add(toChoose[index]);
                toChoose.RemoveAt(index);
            }
        }

        List<GameObject> objects = new List<GameObject>();

        for (int i = 0; i < indices.Count; i++)
        {
            objects.Add((GameObject)GameObject.Instantiate(m_Bosses[indices[i]], new Vector3(100, 100, 100), m_Bosses[indices[i]].transform.localRotation));
        }

        return objects.ToArray();
    }
}
