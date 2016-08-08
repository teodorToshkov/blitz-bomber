using UnityEngine;
using System.Collections;

/*!
 * \brief A script which initializes the sprite and color of the back and front sides of the destroyed floor prefab.
 */
public class DestroyedFloorInitializer : MonoBehaviour
{
	public Sprite sprite;                   //!< the sprite on both sides of the destroyed floor
	public Color color;                     //!< the colour of both sides of the destroyed floor

	[SerializeField]
	private SpriteRenderer frontSprite;     //!< a reference to the sprite on the front side of the destroyed floor
	[SerializeField]
	private SpriteRenderer backSprite;      //!< a reference to the sprite on the back side of the destroyed floor

    [SerializeField]
	private MeshRenderer frontMesh;         //!< a reference to the mesh of the front side of the destroyed floor
    [SerializeField]
	private MeshRenderer backMesh;          //!< a reference to the mesh of the back side of the destroyed floor

    //! We set the colours and sprites of the references to __sprite__ and __color__
    void Start ()
	{
		frontSprite.sprite = sprite;
		backSprite.sprite = sprite;

		frontMesh.material.color = color;
		backMesh.material.color = color;
	}
}
