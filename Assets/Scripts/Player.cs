using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[RequireComponent(typeof(Locomotion))]
public class Player : MonoBehaviour
{
    [HideInInspector]
    public Locomotion locomotion;
    


    private void Awake()
    {
        locomotion = GetComponent<Locomotion>();
    }

    private void Update()
    {
        locomotion.Move(Input.GetAxis("Horizontal"));

        if (Input.GetKeyDown(KeyCode.Space))
            locomotion.Jump();
    }

}
