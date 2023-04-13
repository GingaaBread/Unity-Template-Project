namespace YPPGUtilities
{
    namespace Gameplay
    {
        using UnityEngine;
        using UnityEngine.Events;

        /**
         *  <summary>
         *  Equips GameObjects with the ability to be picked up, triggering the OnCollectedEvent
         *  </summary>          
         */
        public class Collectible : MonoBehaviour
        {
            // Invoked when the GameObject with tag that equals collidingTagName enters this GameObject's collider
            public UnityEvent OnCollectedEvent;

            // The default layer that triggers the collection is "Player"
            public string collidingTagName = "Player";

            // An optional GameObject reserved for particles that can be instantiated after picking up the Collectible
            [SerializeField] private GameObject particles;

            // Used to make sure the collectible is only collected once
            private bool touched;

            // If the GameObject was collected, the particles will be spawned, event will be invoked, and this GameObject will be destroyed
            private void OnTriggerEnter2D(Collider2D collision)
            {
                if (collision.CompareTag(collidingTagName) && !touched)
                {
                    touched = true;
                    if (particles != null)
                    {
                        Instantiate(particles).transform.position = new Vector2(transform.position.x, transform.position.y + 0.1f);
                    }
                    OnCollectedEvent.Invoke();
                    Destroy(gameObject);
                }
            }
        }
    }
}