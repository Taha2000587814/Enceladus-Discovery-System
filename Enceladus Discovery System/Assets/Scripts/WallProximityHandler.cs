using UnityEngine;

public class WallProximityHandler : MonoBehaviour
{
    public bool isPassableWall = false;
    public float collapseDistance = 1.5f;
    public WallMaterialProfile materialProfile;

    private Renderer rend;
    private Collider col;
    private Material defaultMat;
    private Material unstableMat;
    private Transform player;

    void Start()
    {
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider>();
        defaultMat = rend.material;
        unstableMat = isPassableWall ? materialProfile.passableUnstable : materialProfile.unpassableUnstable;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
    }

    public void SetUnstable(bool lookingAt)
    {
        if (lookingAt)
        {
            rend.material = unstableMat;
            rend.material.SetFloat("_Severity", 1f);

            if (isPassableWall && player != null)
            {
                float dist = Vector3.Distance(player.position, transform.position);
                if (dist < collapseDistance)
                {
                    col.enabled = false;
                }
                else
                {
                    col.enabled = true;
                }
            }
        }
        else
        {
            rend.material = defaultMat;
            col.enabled = true;
        }
    }
}
