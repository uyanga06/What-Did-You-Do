using UnityEngine;

public class Mirror : MonoBehaviour
{
    //Reference: Crafting mirrors in Unity : Reflections Tutorial. (2024). [Video] Directed by UnityDude. YouTube. Available at: https://www.youtube.com/watch?v=_jBWm0KM2ec.
    public Transform PlayerCam;
    public Transform MirrorCam;

    void Update()
    {
        Vector3 PosY = new Vector3(transform.position.x, PlayerCam.transform.position.y, transform.position.z);
        Vector3 side1 = PlayerCam.transform.position - PosY;
        Vector3 side2 = transform.forward;
        float angle = Vector3.SignedAngle(side1, side2, Vector3.up);

        MirrorCam.localEulerAngles = new Vector3(0, angle, 0);
    }
}
