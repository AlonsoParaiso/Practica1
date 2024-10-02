using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{

    public int value = 1;
    private int monedaTotal;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<PlayerMovement_rb>())//la moneda suma a la puntuacion total y tiene sonido y luego se destruye
        {
            monedaTotal = GameManager.instance.GetPoints();
            monedaTotal = value + monedaTotal;
            GameManager.instance.SetPoints(monedaTotal);
            Destroy(gameObject);
        }

    }
}