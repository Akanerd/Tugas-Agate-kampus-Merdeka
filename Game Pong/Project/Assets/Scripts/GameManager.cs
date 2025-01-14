﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Pemain 1
    public PlayerControl player1; //skrip
    private Rigidbody2D player1Rigidbody;

    //Pemain 2
    public PlayerControl player2; //skrip
    private Rigidbody2D player2Rigidbody;

    // Bola
    public BallControl ball; // skrip
    private Rigidbody2D ballRigidbody;
    private CircleCollider2D ballCollider;

    // Skor maksimal
    public int maxScore;

    //Apakah debug window ditampilkan?
    private bool isDebugWindowShown = false;

    //Objek untuk menggambar prediksi lintasan bola
    public Trajectory trajectory;

    //Inisialisasi rigidbody dan collider
    private void Start()
    {
        player1Rigidbody = player1.GetComponent<Rigidbody2D>();
        player2Rigidbody = player2.GetComponent<Rigidbody2D>();
        ballRigidbody    = ball.GetComponent<Rigidbody2D>();
        ballCollider     = ball.GetComponent<CircleCollider2D>();
    }

    //Untuk menapilkan GUI
    private void OnGUI()
    {
        //Tampilkan skor pemain 1 dikiri atas dan pemain 2 dikanan atas
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100),"" + player1.Score);
        GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100, 100), "" + player2.Score);

        // Tombol restart untuk memulai game dari awal
        if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART"))
        {
            //Ketika tombol restart untuk memulai game dari awal
            player1.ResetScore();
            player2.ResetScore();

            // dan restart game
            ball.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
        }

        //Jika pemain 1 menang (sudah mencapai skor maksimal duluan)
        if (player1.Score ==  maxScore)
        {
            //Tampilkan teks "Player 1 wins" dibagian kiri layar
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 10, 2000, 1000),"PlAYER ONE WINS");

            // dan kembalikan bola ke tengah
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }
        // Sedangkan jika player 2 menang
        else if(player2.Score == maxScore)
        {
            //Tampilkan teks "Player 2 wins" dibagian kiri layar
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 10, 2000, 1000), "PlAYER TWO WINS");

            // dan kembalikan bola ke tengah
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }

        //Jika idDebugWindowShown == true, Tmapilkan debug window dengan background text area merah
        if (isDebugWindowShown)
        {
            //Simpan nilai warna lama GUI
            Color oldColor = GUI.backgroundColor;

            //Beri warna baru
            GUI.backgroundColor = Color.red;

            //Simpan variabel-variabel fisika yang akan ditampilkan.
            float ballMass       = ballRigidbody.mass;
            Vector2 ballVelocity = ballRigidbody.velocity;
            float ballSpeed      = ballRigidbody.velocity.magnitude;
            Vector2 ballMomentum = ballMass * ballVelocity;
            float ballFriction   = ballCollider.friction;

            float impulsePlayer1X = player1.LastContactPoint.normalImpulse;
            float impulsePlayer1Y = player1.LastContactPoint.tangentImpulse;
            float impulsePlayer2X = player2.LastContactPoint.normalImpulse;
            float impulsePlayer2Y = player2.LastContactPoint.tangentImpulse;

            //Tampilkan debug text-nya
            string debugtext =
                "Ball mass = " + ballMass + "\n" +
                "Ball velocity = " + ballVelocity + "\n" +
                "Ball speed = " + ballSpeed + "\n" +
                "Ball momentum = " + ballMomentum + "\n" +
                "Ball friction = " + ballFriction + "\n" +

                "Last impulse from player 1 = (" + impulsePlayer1X + ", " + impulsePlayer1Y + ")\n" +
                "Last impulse from player 2 = (" + impulsePlayer2X + ", " + impulsePlayer2Y + ")\n";

            //Tampilkan Debug window
            GUIStyle guiStyle = new GUIStyle(GUI.skin.textArea);
            guiStyle.alignment = TextAnchor.UpperCenter;
            GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height - 200, 400, 110), debugtext, guiStyle);

            // Kembalikan warna lama GUI
            GUI.backgroundColor = oldColor;
        }
        // Toggle nilai debug window dan trajectory ketika pemain mengeklik tombol ini.
        if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height - 73, 120, 53), "TOGGLE\nDEBUG INFO"))
        {
            isDebugWindowShown = !isDebugWindowShown;
            trajectory.enabled = !trajectory.enabled;
        }
    }
}



