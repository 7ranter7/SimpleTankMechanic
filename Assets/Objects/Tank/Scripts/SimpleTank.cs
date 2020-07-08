using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SimpleTank : MonoBehaviour
{
    ICannon cannon;
    IMovable movable;


    private Vector3 axises;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        movable = GetComponent<IMovable>();
        cannon = GetComponentInChildren<ICannon>();
    }


    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void FixedUpdate()
    {
        axises = Vector3.zero;
        if (cannon != null)
        {
            RaycastHit hit;
            Debug.Log("WTF");
            var ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            if (Physics.Raycast(ray, out hit, 1000))
                axises = cannon.Rotate(hit.point);

            if (Input.GetMouseButton(1))
            {
                if (cannon.CanFire(hit.point))
                {
                    cannon.Fire(hit.point);
                }
            }
        }
        if (movable != null)
        {
            if (Input.GetKey(KeyCode.W))
                axises.z = 1;
            if (Input.GetKey(KeyCode.S))
                axises.z = -1;
            if (Input.GetKey(KeyCode.D))
                axises.y = 1;
            if (Input.GetKey(KeyCode.A))
                axises.y = -1;
            movable.Move(axises);
        }

    }
}
