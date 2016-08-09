using UnityEngine;
using System.Collections;

public class CameraControll : MonoBehaviour {

    public GameObject m_mainCamera;
    public GameObject m_villageCamera;

    public float m_cameraSpeed = 5.0f;
    public int m_cameraBorderFactor = 10;
    public float m_cameraVelocityFactor = 30.0f;

    public int m_offsetBottom = 300;

    //values for the position and rotation of the main camera
    private float m_translationX1 = -3.0f;
    private float m_translationY1 = 7.0f;
    private float m_translationZ1 = 3.5f;

    private float m_rotationX1 = 50.0f;
    private float m_rotationY1 = 80.0f;
    private float m_rotationZ1 = 0.0f;

    //values for the position and rotation of the village camera
    private float m_translationXVillage = 5.0f;
    private float m_translationYVillage = 5.0f;
    private float m_translationZVillage = -5.0f;

    private float m_rotationXVillage = 50.0f;
    private float m_rotationYVillage = 0.0f;
    private float m_rotationZVillage = 0.0f;

	// Use this for initialization
	void Start () 
    {
        m_mainCamera.transform.Translate(m_translationX1, m_translationY1, m_translationZ1);
        m_mainCamera.transform.Rotate(m_rotationX1, m_rotationY1, m_rotationZ1);
        m_mainCamera.SetActive(true);

        setVillageCamera();
        m_villageCamera.SetActive(false);

	}
	
	// Update is called once per frame
	void Update ()
    {
        //Steuerung mit der Maus

        if (m_mainCamera.activeSelf)
        {
            Vector3 mousePos = Input.mousePosition;

            if (mousePos.x < (Screen.width / m_cameraBorderFactor) && mousePos.y > m_offsetBottom)
            {
                float value = (Screen.width / m_cameraBorderFactor) - mousePos.x;
                float speed = m_cameraSpeed * (value / m_cameraVelocityFactor);
                m_mainCamera.transform.position += new Vector3(0.0f, 0.0f, speed * Time.deltaTime);
            }
            else if (mousePos.x > (Screen.width - (Screen.width / m_cameraBorderFactor)) && mousePos.y > m_offsetBottom)
            {
                float value = mousePos.x - (Screen.width - (Screen.width / m_cameraBorderFactor));
                float speed = m_cameraSpeed * (value / m_cameraVelocityFactor);
                m_mainCamera.transform.position += new Vector3(0.0f, 0.0f, -speed * Time.deltaTime);
            }
        }

        //Mausrad drücken
        if(Input.GetMouseButtonUp(2))
        {
            if(m_mainCamera.activeSelf)
            {
                m_mainCamera.SetActive(false);
                setVillageCamera();
                m_villageCamera.SetActive(true);
            }
            else
            {
                m_mainCamera.SetActive(true);
                m_villageCamera.SetActive(false);
            }
        }


        //ALT: Steuerung mit der Tastatur
        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    if (m_mainCamera.activeSelf)
        //    {
        //        m_mainCamera.SetActive(false);
        //        setVillageCamera();
        //        m_villageCamera.SetActive(true);
        //    }
        //    else
        //    {
        //        m_mainCamera.SetActive(true);
        //        m_villageCamera.SetActive(false);
        //    }
        //}

        //if (m_mainCamera.activeSelf)
        //{
        //    if (Input.GetKey(KeyCode.A))
        //    {
        //        //transform.Translate(new Vector3(-(m_cameraSpeed * Time.deltaTime), 0.0f, 0.0f));
        //        m_mainCamera.transform.position += new Vector3(0.0f, 0.0f, m_cameraSpeed * Time.deltaTime);
        //    }
        //    if (Input.GetKey(KeyCode.D))
        //    {
        //        //Vector3 currPos = m_mainCamera.transform.position;
        //        //Vector3 toAdd = new Vector3(0.0f, 0.0f, -(m_cameraSpeed * Time.deltaTime));
        //        //Vector3 toGoTo = currPos + toAdd;
        //        //m_mainCamera.transform.position = Vector3.Lerp(currPos, toGoTo, Time.deltaTime * 50f);
        //        m_mainCamera.transform.position += new Vector3(0.0f, 0.0f, -(m_cameraSpeed * Time.deltaTime));
        //    }
        //}
        //else
        //{
        //    if (Input.GetKey(KeyCode.W))
        //    {
        //        m_villageCamera.transform.position += new Vector3(0.0f, 0.0f, m_cameraSpeed * Time.deltaTime);
        //    }
        //    if (Input.GetKey(KeyCode.S))
        //    {
        //        m_villageCamera.transform.position += new Vector3(0.0f, 0.0f, -(m_cameraSpeed * Time.deltaTime));
        //    }
        //}
	}

    private void setVillageCamera()
    {
        m_villageCamera.transform.position = new Vector3(m_translationXVillage, m_translationYVillage, m_translationZVillage);
        m_villageCamera.transform.eulerAngles = new Vector3(m_rotationXVillage, m_rotationYVillage, m_rotationZVillage);
    }
}
