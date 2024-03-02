using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private bool onGround;
    private float friction;

    public bool GetOnGround(){
        return onGround;
    }
    public float GetFriction(){
        return friction;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        EvaluateCollision(other);
        RetrieveFriction(other);
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        EvaluateCollision(other);
        RetrieveFriction(other);

    }
    private void OnCollisionExit2D(Collision2D other)
    {
        onGround = false;
        friction = 0;
    }
    private void EvaluateCollision(Collision2D collision2D){
        for (int i = 0; i < collision2D.contactCount; i++){
            Vector2 normal = collision2D.GetContact(i).normal;
            onGround |= normal.y >= 0.9f;
        }
    }
    private void RetrieveFriction(Collision2D collision){
        PhysicsMaterial2D material = collision.rigidbody.sharedMaterial;
        friction = 0f;
        if(material != null){
            friction = material.friction;
        }
    }
}
