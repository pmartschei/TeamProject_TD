using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveGroup : IWave
{

	public List<IWave> m_Waves=new List<IWave>();//Alle Unter-Wellen

	public WaveGroup ()
	{
	}

	//alle Wellen prüfen ob ein GameObject erstellt werden kann
	public GameObject[] Update(){
		List<GameObject> gos = new List<GameObject> ();
		foreach (IWave wave in m_Waves) {
			GameObject[] res = wave.Update ();
			if (res != null) {//wenn die Unter-Welle GameObjekte schicken kann, dann hinzufügen
				gos.AddRange (res);
			}
		}
		return gos.ToArray ();
	}

	//sind alle Unter-Wellen geschickt
	public bool Clear(){
		foreach (IWave wave in m_Waves) {
			if (!wave.Clear()) {
				return false;
			}
		}
		return true;
	}

}

