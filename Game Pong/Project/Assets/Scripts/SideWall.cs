﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWall : MonoBehaviour
{
    // Pemain yang akan bertambah skornya jika bola menyentuh dinding ini
    public PlayerControl player;
 
    // berfungsi untuk mengakses game manager guna skor maksimal
    private GameManager gameManager;

    void Start()
    {
        //Inisialisasi variabel gameManager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Akan dipanggil ketika objek lain ber-collider (bola) berentuhan dengan dinding.
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Jika objek tersebut bernama 'Ball'
        if (hitInfo.name == "Ball")
        {
            // Tambahkan skor ke pemain
            player.IncrementScore();

            //Jika skor pemain belum mencapai skor maksimal ....
            if (player.Score < gameManager.maxScore)
            {
                //... Restart game setelah bola mengenai dinding
                hitInfo.gameObject.SendMessage("RestartGame", 2.0f, SendMessageOptions.RequireReceiver);
            }
        }
    }
}
