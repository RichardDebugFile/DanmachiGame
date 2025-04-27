using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{

    public GameObject Murcielago;

    void Update()
    {
        if (Murcielago != null)
        {
            Vector3 position = transform.position;
            position.x = Murcielago.transform.position.x;
            transform.position = position;
        }
    }
}
