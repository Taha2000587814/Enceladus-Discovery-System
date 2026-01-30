using UnityEngine;

public class CameraWallDetector : MonoBehaviour
{
    public float maxRayDistance = 20f;
    public LayerMask wallLayer;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxRayDistance, wallLayer))
        {
            WallProximityHandler wall = hit.collider.GetComponent<WallProximityHandler>();
            if (wall != null)
            {
                wall.SetUnstable(true);
            }
        }
        else
        {
         
            WallProximityHandler[] walls = FindObjectsOfType<WallProximityHandler>();
            foreach (var w in walls) w.SetUnstable(false);
        }
    }
}
