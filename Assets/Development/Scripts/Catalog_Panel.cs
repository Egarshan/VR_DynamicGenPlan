using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Catalog_Panel : PanelController
{
    public Button Lamp_Btn, Window_Btn, Door_Btn, Interior_Btn;
    private Button backBtn;
    private GameObject currentObj = null;
    private GameObject IntPan;

    public Material correctMat, wrongMat;

    int mode = 0;   // 1 - window, 2 - door, 3 - lamp

    private void Start()
    {
        backBtn = GameObject.Find("Back_btn").GetComponent<Button>();

        IntPan = GameObject.Find("IntObj_Panel");

        backBtn.onClick.AddListener(SwitchToMenu);

        Interior_Btn.onClick.AddListener(OpenInteriorPanel);
        Window_Btn.onClick.AddListener(WindowMode);
        Door_Btn.onClick.AddListener(DoorMode);
        Lamp_Btn.onClick.AddListener(LampMode);

        IntPan = GameObject.Find("IntObj_Panel");
        GameObject.Find("Catalog_Panel").SetActive(false);
    }

    private void Update()
    {
        Spawn(currentObj.GetComponent<Object>().colTag, currentObj.GetComponent<Object>().constraintAxis);
    }
    public void SetCurrentInteriorObj(GameObject InteriorObj)
    {
        currentObj = InteriorObj;
    }

    private void LampMode()
    {
        if (mode == 3)
        {
            Reset();
        }
        else
        {
            Reset();
            currentObj = GameObject.Find("Lamp");
            mode = 3;
            Lamp_Btn.transform.GetChild(0).GetComponent<Image>().enabled = true;
        }
    }

    private void WindowMode()
    {
        if (mode == 1)
        {
            Reset();
        }
        else
        {
            Reset();
            currentObj = GameObject.Find("Window");
            mode = 1;
            Window_Btn.transform.GetChild(0).GetComponent<Image>().enabled = true;
        }
    }

    private void DoorMode()
    {
        if (mode == 2)
        {
            Reset();
        }
        else
        {
            Reset();
            currentObj = GameObject.Find("Door");
            mode = 2;
            Door_Btn.transform.GetChild(0).GetComponent<Image>().enabled = true;
        }
    }

    private void OpenInteriorPanel()
    {
        Reset();
        this.transform.localScale = new Vector3(0, 0, 0);
        IntPan.transform.localScale = new Vector3(1, 1, 1);
    }

    private void Spawn(string tag, int axis = 0)    // 0 - no constaint, 1 - x, 2 - y, 3 -z
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        Vector3 constraintVect;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.tag == tag)
                {
                    currentObj.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
                    currentObj.GetComponent<BoxCollider>().enabled = true;

                    constraintVect = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z);
                    switch (axis)
                    {
                        case 1:
                            constraintVect.x = 0;
                            break;

                        case 2:
                            constraintVect.y = 0;
                            break;

                        case 3:
                            constraintVect.z = 0;
                            break;

                        default:
                            break;
                    }

                    Instantiate(currentObj, constraintVect, Quaternion.FromToRotation(Vector3.forward, hitInfo.normal));
                    
                    currentObj.GetComponent<BoxCollider>().enabled = false;
                    currentObj.transform.GetChild(0).GetComponent<Renderer>().enabled = true;
                }
            }
        }

        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.tag != tag)
                currentObj.transform.GetChild(0).GetComponent<Renderer>().material = wrongMat;
            else
            {
                currentObj.transform.GetChild(0).GetComponent<Renderer>().material = correctMat;

                constraintVect = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z);
                switch (axis)
                {
                    case 1:
                        constraintVect.x = 0;
                        break;

                    case 2:
                        constraintVect.y = 0;
                        break;

                    case 3:
                        constraintVect.z = 0;
                        break;

                    default:
                        break;
                }

                currentObj.transform.position = constraintVect;
                currentObj.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hitInfo.normal);
            }     
        }
    }

    public void Reset()
    {
        mode = 0;
        if (currentObj != null)
        {
            currentObj.transform.position = new Vector3(12.25f, 0, 0);
            currentObj = null;
        }

        Window_Btn.transform.GetChild(0).GetComponent<Image>().enabled = false;
        Door_Btn.transform.GetChild(0).GetComponent<Image>().enabled = false;
        Lamp_Btn.transform.GetChild(0).GetComponent<Image>().enabled = false;
    }

    private void SwitchToMenu()
    {
        Reset();
        Switch();
    }
}
