    using UnityEngine;
    using System.Collections;

    public class MovingPlatform : MonoBehaviour
    {
        public enum Direction { Up, Down, Left, Right }

        [Header("Settings")]
        [SerializeField][Range(1,10)] private float distance = 2f;
        [SerializeField][Range(1, 5)] private float speed = 2f;
        [SerializeField] private float waitTime = 1f;
        [SerializeField] private Direction startDirection = Direction.Up;

        private Vector3 startPos;
        private Vector3 targetPos;
        private Vector3 dir;





        void Start()
        {

            startPos = transform.position;
            dir = GetDirection(startDirection);
            targetPos = startPos + dir * distance;
            StartCoroutine(Move());
        }

        IEnumerator Move()
        {
            while (true)
            {
                yield return MoveTo(targetPos);

                (startPos, targetPos) = (targetPos, startPos);

                yield return new WaitForSeconds(waitTime);
            }
        }

        IEnumerator MoveTo(Vector3 target)
        {
            while (Vector3.Distance(transform.position, target) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    target,
                    speed * Time.deltaTime
                );

                yield return null;
            }
        }

        Vector3 GetDirection(Direction d)
        {
            return d switch
            {
                Direction.Up => Vector3.up,
                Direction.Down => Vector3.down,
                Direction.Left => Vector3.left,
                Direction.Right => Vector3.right,
                _ => Vector3.up
            };
        }


    // Move objects with platform.


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.gameObject.transform.parent = null;
    }



}