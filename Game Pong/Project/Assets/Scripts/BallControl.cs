﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    // Deklarasi variabel
    //Rigidbody 2D bola
    private Rigidbody2D rigidBody2D;
    
    //Besarnya gaya awal yang diberikan untuk mendorong bola
    public float xInitialForce;
    public float yInitialForce;

    private Vector2 trajectoryOrigin;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        trajectoryOrigin = transform.position;

        // Mulai game
        RestartGame();
    }

    void ResetBall()
    {
        // Reset posisi menjadi  (0, 0)
        transform.position = Vector2.zero;

        // Reset kecepatan menjadi (0, 0)
        rigidBody2D.velocity = Vector2.zero;
    }

    void PushBall()
    {
        // float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);
        // Mengganti Gaya Y awal yang tadinya di Random, menjadi fixed

        // Tentukan nilai acak antara 0 (inklusif) dam 2 (ekslusif)
        float randomDirection = Random.Range(0, 2);

        // Jika nilainya dibawah 1, bola bergerak ke kiri
        // JIka tidak, bola bergerak ke kanan
        if (randomDirection < 1.0f)
        {
            // Gunakan gaya untuk menggerakkan bola ini
            rigidBody2D.AddForce(new Vector2(-xInitialForce, -yInitialForce));
        }
        else
        {
            rigidBody2D.AddForce(new Vector2(xInitialForce, -yInitialForce));
        }
    }

    void RestartGame()
    {
        // Kembalikan bola ke posisi semula
        ResetBall();

        // Setalah 2 detik, berikan gaya ke bola
        Invoke("PushBall", 2);
    }

    //Ketika bola bernjak dari sebuah tumbukan, rekam titik tumbukan tersebut
    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }

    // Untuk mengakses informasi titik asal lintasan
    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }
}
