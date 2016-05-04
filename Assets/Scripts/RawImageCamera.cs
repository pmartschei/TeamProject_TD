using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RawImageCamera : MonoBehaviour {

	// Use this for initialization

    public Camera m_MainCamera;
    public Camera m_ImageCamera;
    public RawImage m_Image;
    [SerializeField]
    public GameObjectHitEvent m_OnGameObjectHit;
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (!Input.GetMouseButton(0))
            return;
        Vector3[] corners = new Vector3[4];
        m_Image.rectTransform.GetWorldCorners(corners);
        Rect newRect = new Rect(corners[0], corners[2] - corners[0]);
        if (newRect.Contains(Input.mousePosition))//überprüft ob maus in rechteck
        {
            Vector3 abs = Input.mousePosition - corners[0];//absoluter wert
            Vector3 divider = corners[2] - corners[0];//größe
            abs.x /= divider.x;
            abs.y /= divider.y;//auf UV coordinaten
            Ray ray;
            RaycastHit hit1;
            ray = m_ImageCamera.ViewportPointToRay(new Vector3(abs.x, abs.y, 0f));//2. camera
            if (Physics.Raycast(ray, out hit1))
            {
                m_OnGameObjectHit.Invoke(hit1.transform.gameObject);
            }
        }
	}
}
