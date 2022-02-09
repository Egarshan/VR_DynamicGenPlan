using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interior_Panel : PanelController
{
    public Button Bathroom_btn, Furniture_btn, Kitchen_btn, NextBtn, PreviousBtn;

    public GameObject iconPlace, scroller_panel;
    private GameObject CatalogPan;

    public Sprite[] Bathroom_icons;
    public Sprite[] Furniture_icons;
    public Sprite[] Kitchen_icons;

    private Sprite[] Active_icons;
    public Button backBtn;

    int mode = 0, iconIndex;   // 1 - Bathroom, 2 - Furniture, 3 - Kitchen

    void Start()
    {
        CatalogPan = GameObject.Find("Catalog_Panel");

        Bathroom_btn.onClick.AddListener(ActivateBathroomCategory);
        Furniture_btn.onClick.AddListener(ActivateFurnitureCategory);
        Kitchen_btn.onClick.AddListener(ActivateKitchenCategory);

        NextBtn.onClick.AddListener(NextObj);
        PreviousBtn.onClick.AddListener(PreviousObj);

        backBtn.onClick.AddListener(SwitchToCatalog);
    }
    private void ActivateBathroomCategory()
    {
        if (mode == 1)
        {
            Reset();
        }
        else
        {
            Reset();
            Bathroom_btn.transform.GetChild(0).GetComponent<Image>().enabled = true;
            mode = 1;
            ActivateObjScroller(1);
        }
    }
    private void ActivateFurnitureCategory()
    {
        if (mode == 2)
        {
            Reset();
        }
        else
        {
            Reset();
            Furniture_btn.transform.GetChild(0).GetComponent<Image>().enabled = true;
            mode = 2;
            ActivateObjScroller(2);
        }
    }
    private void ActivateKitchenCategory()
    {
        if (mode == 3)
        {
            Reset();
        }
        else
        {
            Reset();
            Furniture_btn.transform.GetChild(0).GetComponent<Image>().enabled = true;
            mode = 3;
            ActivateObjScroller(3);
        }
    }
    private void Reset()
    {
        mode = 0;
        scroller_panel.SetActive(false);
        CatalogPan.GetComponent<Catalog_Panel>().Reset();

        Bathroom_btn.transform.GetChild(0).GetComponent<Image>().enabled = false;
        Furniture_btn.transform.GetChild(0).GetComponent<Image>().enabled = false;
        Kitchen_btn.transform.GetChild(0).GetComponent<Image>().enabled = false;
    }
    private void ActivateObjScroller(int mode)
    {
        scroller_panel.SetActive(true);

        switch (mode)
        {
            case 1:
                Active_icons = Bathroom_icons;
                break;

            case 2:
                Active_icons = Furniture_icons;
                break;

            case 3:
                Active_icons = Kitchen_icons;
                break;

            default:
                break;
        }
        iconIndex = 0;
        iconPlace.GetComponent<Image>().sprite = Active_icons[iconIndex];
        CatalogPan.GetComponent<Catalog_Panel>().SetCurrentInteriorObj(GameObject.Find(iconPlace.GetComponent<Image>().sprite.name));
    }
    private void SwitchToCatalog()
    {
        Reset();
        this.transform.localScale = new Vector3(0, 0, 0);
        CatalogPan.transform.localScale = new Vector3(1, 1, 1);
    }
    private void NextObj()
    {
        if (iconIndex != (Active_icons.Length - 1))
            CatalogPan.GetComponent<Catalog_Panel>().Reset();
        if (iconIndex < (Active_icons.Length-1))
        {
            iconPlace.GetComponent<Image>().sprite = Active_icons[++iconIndex];
            CatalogPan.GetComponent<Catalog_Panel>().SetCurrentInteriorObj(GameObject.Find(iconPlace.GetComponent<Image>().sprite.name));
        }
    }
    private void PreviousObj()
    {
        if (iconIndex != 0)
            CatalogPan.GetComponent<Catalog_Panel>().Reset();
        if (iconIndex > 0)
        {
            iconPlace.GetComponent<Image>().sprite = Active_icons[--iconIndex];
            CatalogPan.GetComponent<Catalog_Panel>().SetCurrentInteriorObj(GameObject.Find(iconPlace.GetComponent<Image>().sprite.name));
        }
    }
}
