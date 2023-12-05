using UnityEngine;

namespace Noah.Scripts
{
    public class MovableBox : MonoBehaviour
    {
        private void OnCollisionStay2D(Collision2D other)
        {
            if (Tags.CompareTags("Player", other.gameObject))
            {
                GetComponent<Rigidbody2D>().isKinematic = false;
                other.gameObject.GetComponent<RelativeJoint2D>().enabled = true;
                other.gameObject.GetComponent<RelativeJoint2D>().connectedBody = GetComponent<Rigidbody2D>();
            }        
        }
    }
}
