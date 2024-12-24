using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AzarashiController : MonoBehaviour
{
    Rigidbody2D rb2d;

    public float maxHeight;
    public float flapVelocity;

    private void Awake()
    {
        //Startより前の段階でRigidbody2D
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //最高高度に達していない場合に限りタップの入力を受け付ける
        if(Input.GetButtonDown("Fire1") && transform.position.y < maxHeight)
        {
            Flap();
        }
    }

    //上空にはばたく動きのメソッド
    public void Flap()
    {
        //Velocityに力を与えて上方向に動かす
        rb2d.velocity = new Vector2(0.0f, flapVelocity);
    }
}
