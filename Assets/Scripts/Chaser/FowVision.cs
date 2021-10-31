using UnityEngine;

namespace Chaser
{
    public class FowVision : MonoBehaviour
    {

        private FOW fow;
        float angleIncrements;
        [SerializeField] private int rayCount = 200;


        private Vector3 origin;

        private Mesh mesh;
        [SerializeField] private Material lightMaterial;

        private float startingAngle;

        void Awake()
        {
            fow = gameObject.GetComponent<FOW>();

            CreateFowVisionMesh();
        }


        void LateUpdate()
        {
            UpdateFowVision();
        }

        private void CreateFowVisionMesh()
        {
            mesh = new Mesh();
            angleIncrements = fow.viewAngle / (rayCount);

            gameObject.GetComponent<MeshFilter>().mesh = mesh;
            gameObject.GetComponent<MeshRenderer>().material = lightMaterial;

            origin = Vector3.zero;
        }

        private void UpdateFowVision()
        {
            mesh.Clear();

            float angle = startingAngle;

            origin.z = 0;

            Vector3[] vertices = new Vector3[rayCount + 1 + 1];
            Vector2[] uv = new Vector2[vertices.Length];
            int[] triangles = new int[rayCount * 3];

            // origin -= transform.parent.localPosition;
            vertices[0] = origin - transform.parent.position;

            int vertexIndex = 1;
            int triangleIndex = 0;
            for (int i = 0; i <= rayCount; i++)
            {
                Vector3 vertex;
                Vector3 dirFromAngle = GetVectorFromAngle(angle);

                RaycastHit2D hitInfo = Physics2D.Raycast(origin, dirFromAngle, fow.viewRadius);
                // Debug.DrawRay(transform.position, dirFromAngle, Color.black);
                if (hitInfo.collider == null)
                {
                    vertex = (origin - transform.parent.position) + dirFromAngle * fow.viewRadius;
                }
                else
                {
                    vertex = hitInfo.point - (Vector2)transform.parent.position;
                }

                vertices[vertexIndex] = vertex;

                if (i > 0)
                {
                    triangles[triangleIndex + 0] = 0;
                    triangles[triangleIndex + 1] = vertexIndex - 1;
                    triangles[triangleIndex + 2] = vertexIndex;

                    triangleIndex += 3;
                }

                vertexIndex++;
                angle -= angleIncrements;
            }

            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;
        }

        public void SetOrigin(Vector3 _origin)
        {
            origin = _origin;
        }

        private Vector3 GetVectorFromAngle(float angle)
        {
            float angleRad = angle * (Mathf.PI / 180f);
            return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
        }


        public void SetAimDirection(Vector3 aimDirection)
        {
            startingAngle = GetAngleFromVectorFloat(aimDirection) + fow.viewAngle / 2;
        }

        private float GetAngleFromVectorFloat(Vector3 dir)
        {
            dir = dir.normalized;
            float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (n < 0) n += 360;

            return n;
        }
    }
}