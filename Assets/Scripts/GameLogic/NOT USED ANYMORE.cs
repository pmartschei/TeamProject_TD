using UnityEngine;
using System.Collections;

public class UISelectTile : MonoBehaviour
{
    private GameObject clone;

    void Start()
    {

    }

    // Testen der Größe der Collidor Box
    public void OnMouseOver()
    {
        Debug.Log("MouseOver");
    }


    public void OnMouseDown()
    {
        Debug.Log("Maus geklickt");
        clone = Instantiate(gameObject);
        Debug.Log(clone);
        clone.transform.rotation = Quaternion.Euler(-90, 0, 0);
        GameObject.Find("MouseSystem").GetComponent<MouseScript>().SetTile(clone);
        DestroyImmediate(clone);
        Debug.Log(clone);
    }
}
