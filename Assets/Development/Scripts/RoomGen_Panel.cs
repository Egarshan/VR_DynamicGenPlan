using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomGen_Panel : PanelController
{
    InputField height, width, depth;
    GameObject workPad;
    public GameObject errorField, roomBase, entranceDoor;
    Button genBtn;

    bool isCorrect;
    float[] sizes = new float[3]; //0 - h, 1 - w, 2 - d

    private void Start()
    {
        height = GameObject.Find("Height_input").GetComponent<InputField>();
        width = GameObject.Find("Width_input").GetComponent<InputField>();
        depth = GameObject.Find("Depth_input").GetComponent<InputField>();

        workPad = GameObject.Find("WorkPad");

        genBtn = switchBtn;
        genBtn.onClick.AddListener(Generate);
    }

    private bool CheckInputs()
    {
        sizes[0] = float.Parse(height.text.Replace('.', ','));
        sizes[1] = float.Parse(width.text.Replace('.', ','));
        sizes[2] = float.Parse(depth.text.Replace('.', ','));

        if (sizes[0] == 0 || sizes[1] == 0 || sizes[2] == 0)
            return false;

        return true;
    }
    private void Generate()
    {
        if (CheckInputs())
        {
            roomBase.transform.localScale = new Vector3(sizes[1] * 0.1f, sizes[0] * 0.1f, sizes[2] * 0.1f);

            Vector3 pos = roomBase.transform.GetChild(3).position;
            pos.y = 0;

            entranceDoor.transform.position = pos;
            
            roomBase.SetActive(true);

            errorField.SetActive(false);
            Switch();
            //workPad.GetComponent<Rigidbody>().useGravity = true;
        }
        else errorField.SetActive(true);
    }
}
