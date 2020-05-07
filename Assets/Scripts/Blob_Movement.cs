using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class Blob_Movement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidbody;
    BoxCollider2D obstacleCollider;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        obstacleCollider = GetComponent<BoxCollider2D>();
        myRigidbody.velocity = new Vector2(moveSpeed, 0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8) //  8 - Ground Layer
        {
            this.transform.localScale = new Vector2(-Mathf.Sign(myRigidbody.velocity.x), 1f);
            myRigidbody.velocity = new Vector2(-Mathf.Sign(myRigidbody.velocity.x) * moveSpeed, 0f);
        }
    }
}
