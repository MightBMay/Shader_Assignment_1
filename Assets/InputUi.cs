using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputUi : MonoBehaviour
{
    public static InputUi instance;
    [SerializeField] GameObject inspecting, notinspecting, notholding, holding;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null) Destroy(instance);
        instance = this;
    }


    private void Start()
    {
        PlayerController.instance.InspectEvent += InspectingCheck;
        PlayerController.instance.HoldingEvent += HoldingCheck;
    }


    void NotHolding()
    {
        holding.SetActive(false);
        notinspecting.SetActive(true);
        inspecting.SetActive(false);
        notholding.SetActive(true);
    }
    void HoldingCheck(bool holding)
    {
        if (holding) Holding();
        else NotHolding();
    }
    void InspectingCheck(bool inspecting)
    {
        if(inspecting) { Inspecting(); }
        else { Holding(); }
    }


    void Holding()
    {
        holding.SetActive(true);
        notinspecting.SetActive(true);
        inspecting.SetActive(false);
        notholding.SetActive(false);
    }

    void Inspecting()
    {
        holding.SetActive(true);
        notinspecting.SetActive(false);
        inspecting.SetActive(true);
        notholding.SetActive(false);
    }


}
