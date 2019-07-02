using UnityEngine;

public class DrawBox : MonoBehaviour
{
    public Vector3 scale;

    /// <summary>
    /// Draws a box to show where an enemy will spawn
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.color = Color.Lerp(Color.cyan, Color.clear, 0.5f);
        Gizmos.DrawCube(Vector3.up * scale.y / 2f, scale);
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(Vector3.zero, Vector3.forward * 0.4f);
    }
}