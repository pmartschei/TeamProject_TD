using UnityEngine;
using System.Collections;

public class MouseScript : MonoBehaviour
{

    // Use this for initialization
    public Camera m_Camera;
    public GameObject m_Rigidbody;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject temp = Instantiate(m_Rigidbody);
        Mesh mesh = temp.GetComponent<MeshFilter>().mesh;
        Vector3 size = mesh.bounds.size;
        GameObject.Destroy(temp);
        Ray r = m_Camera.ScreenPointToRay(Input.mousePosition);
        Vector3 hit = r.origin + Mathf.Abs(r.origin.y/r.direction.y)*r.direction;
        bool xNegative;
        bool yNegative;
        xNegative=hit.x<0.0f;
        yNegative=hit.z<0.0f;
        int x = (int)(hit.x/size.x);
        int y = (int)(hit.z/size.y);
        if (Input.GetMouseButtonDown(0))
        {
            GameObject rb = Instantiate(m_Rigidbody);
            float xMultiplier = 1.0f;
            float yMultiplier = 1.0f;
            if (x <= 0)
            {
                if (xNegative)
                xMultiplier = -1.0f;
            }
            if (y <= 0)
            {
                if (yNegative)
                yMultiplier = -1.0f;
            }
            Vector3 pos = new Vector3(x*size.x +size.x/2.0f*xMultiplier, -size.z/2.0f, y*size.y+size.y/2.0f*yMultiplier);
            Transform t = rb.GetComponent<Transform>();
            Material m = rb.GetComponent<Renderer>().material;
            if (x == 0 || y == 0)
            {
                m.color = Color.white;
            }
            else
            {
                m.color = new Color((255 / (float)x) / 255, (255 / (float)y) /255, 0.0f, 1.0f);
            }
            if (t != null)
            {

                t.position = pos;
            }
        }
    }
}

