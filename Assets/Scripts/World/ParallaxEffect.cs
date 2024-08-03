using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    /*
   Allows the background images to scroll in relation to the application camera.
   GIves the illusion of depth and is essential for a 2D game.
   */

    public Camera cam;
    public Transform followTarget;
    //Starting position for the parallax game object
    Vector2 startingPosition;
    //Start Z value of the parallax game object
    float startingZ;
    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition;
    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;
    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
