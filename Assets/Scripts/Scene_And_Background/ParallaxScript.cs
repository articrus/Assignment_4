using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written By: Gianni Coladonato
 * ID: 2414537
 * Last Modified (Date|ID): 09-11-2024, 2414537
 */
//This scipt is used to control the background elements to create the parallax effect
public class ParallaxScript : MonoBehaviour
{
    //Object components
    public Camera cam;
    public Transform followTar;

    //Variables
    Vector2 startPosition;
    Vector2 camDistance => (Vector2)cam.transform.position - startPosition; //How far the camera has moved from the stating position
    float startingZ;
    float zDistance => transform.position.z - followTar.position.z;
    float clippingPane => (cam.transform.position.z + (zDistance > 0 ? cam.farClipPlane : cam.nearClipPlane));
    float parallaxFactor => Mathf.Abs(zDistance) / clippingPane;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = startPosition + camDistance * parallaxFactor;
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
