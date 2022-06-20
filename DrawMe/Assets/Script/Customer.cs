using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    new Rigidbody2D rigidbody;
    public Sprite[] sprite; 
    public string[] comment;
    
    private string now_comment;
    public static float satisfaction; //****

    public float good_money;
    public float bad_money;

    public static bool object_is_destory;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = Vector2.up * 1600;
        satisfaction = -1;
        object_is_destory = true;

    }
    private void Update()
    {
        if (transform.position.y > 350)
        {
            rigidbody.velocity = Vector2.zero;
        }

    }



    void MoveDown()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - 100);
        rigidbody.velocity = Vector2.down * 1600;
    }

    public void Reaction(float satisfaction)
    {
        //1. 이미지 바뀌기 / 대사 바뀌기 (시간차)
        //2. 금액 지불하기
        //3. 밑으로 사라지기 (객체 삭제)

        if (satisfaction > 50)
        {
            spriteRenderer.sprite = sprite[0];  //만족
            now_comment = comment[2];
            //만족스러운 금액 지불
            Money.AddMoney(good_money);

        }
        else
        {
            spriteRenderer.sprite = sprite[1]; //불만족
            now_comment = comment[2];
            //불만족스러운 금액 지불
            Money.AddMoney(bad_money);
        }
        Invoke("MoveDown", 1f);
        Destroy(this.gameObject, 1.5f);
    }


    public string getNowComment()
    {   
        return now_comment;
    }
}