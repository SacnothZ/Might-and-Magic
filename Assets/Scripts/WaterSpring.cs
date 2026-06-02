using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;


public class WaterSpring : MonoBehaviour
{
        

    [SerializeField] private /*static*/ SpriteShapeController spriteShapeController = null;
    [Header("Values")]
    public float velocity=0;
    public float force=0;
    public float height=0f;           // current height
    public float target_height=0f;   // normal height
    public float resistance=40f;

    private int waveIndex=0;










    public void WaveSpringUpdate(float springStiffness, float dampening)                
    {
        height=transform.localPosition.y;
            
        var x=height-target_height; // maximum extension
        var loss= -dampening * velocity;

        force= -springStiffness * x + loss;
        velocity+=force;
        var y=transform.localPosition.y;
        transform.localPosition = new Vector3(transform.localPosition.x, y + velocity, transform.localPosition.z);

    }


    public void Init(SpriteShapeController ssc)     // calling everytime we create waterpoint, pasing spriteshape controller
    {
        var index = transform.GetSiblingIndex();        // setting valuesto default
        waveIndex = index + 1;
        spriteShapeController = ssc;

        velocity = 0;
        height = transform.localPosition.y;
        target_height = transform.localPosition.y;
    }

    public void WavePointUpdate()           // Animate the poitns in spline.
    {
        if (spriteShapeController != null)
        {
            Spline waterSpline = spriteShapeController.spline;
            Vector3 wavePosition = waterSpline.GetPosition(waveIndex);
            waterSpline.SetPosition(waveIndex, new Vector3(wavePosition.x, transform.localPosition.y, wavePosition.z));
        }
    }


      private void OnCollisionEnter2D(Collision2D other)        //When player touches springs
     {
         if (other.gameObject.tag.Equals("Player"))
          {
           HeroKnight fallingObject = other.gameObject.GetComponent<HeroKnight>();
           Rigidbody2D rb = fallingObject.GetComponent<Rigidbody2D>();
           var speed = rb.linearVelocity;
           velocity += speed.y / resistance;
            }
    }
}
