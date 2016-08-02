using System;
using UnityEngine;

public class Wave : IWave
{
	public GameObject m_Enemy;//Objekt der Welle
	public int m_Count;//Anzahl
	public int m_LeftCount;//Übrige Anzahl
	public float m_Delay;//Timer für nächstes Objekt
	public float m_CurrentDelay;//Restlicher Timer

	public Wave ()
	{
	}

	public Wave(int count,GameObject go,float delay){
		this.m_Count = count;
		this.m_LeftCount = count;
		this.m_Delay = delay;
		this.m_CurrentDelay = delay;
		this.m_Enemy = go;
	}

	public GameObject[] Update(){
		if (Clear())
			return null;
		m_CurrentDelay = Math.Max (m_CurrentDelay - Time.deltaTime, 0);//Spawn timer verringern
		if (m_CurrentDelay == 0) {
			m_CurrentDelay = m_Delay;//Spawn timer setzen
			m_LeftCount--;//übrigen Counter verringern
			return new GameObject[]{m_Enemy};
		} else {
			return null;
		}
	}

	public bool Clear(){
		return m_LeftCount == 0;//Wenn kein spawn mehr übrig
	}
}

