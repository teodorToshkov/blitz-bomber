using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UI
{

    /*! \brief A script that generates 3D Text
     * 
     */
    [ExecuteInEditMode]
    public class ThreeDText : MonoBehaviour
    {
        public enum Ancor               //!< Ancoring text to left, middle or right
        {
            left,                       //!< Left ancor
            middle,                     //!< Middle ancor
            right                       //!< Right ancor
        }

        public Transform parent;        //!< the position of the parent is the at which the last digit stands
        public Vector3 offest;          //!< the offset which the digits should have between each other
        public GameObject[] letters;    //!< the models of the letters

        public string text;             //!< the string value of the text we will display
        public Ancor ancor;             //!< Where should the text be alligned to?
        public float scale = 1;         //!< the scale of the letters

        public Material material;       //!< the material of the letters
        public Color color;             //!< the color of the letters
        
        private List<GameObject> children;  //!< the __GameObject's__ that we spawn as children of __parent__
        private Material _material;     //!< the new material that we create for the letters

        //! Sets _material to a new __Material__
        void Start()
        {
            _material = new Material(material);
            children = new List<GameObject>();
            foreach (Transform item in parent.transform)
            {
                children.Add(item.gameObject);
            }
        }
        
        //! Destroys the previously displayed letters and instantiates the new text
        /*!
         * Resets the color of the material and destroys all elements from __chilrdern__.\n
         * Then it goes through each character from __text__ and spanw the GameObject of letter we are looking at.
         */
        void Update()
        {
            _material.color = color;

            foreach (GameObject child in children)
            {
                DestroyImmediate(child);
            }

            GameObject letter;
            GameObject newLetter;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ' ')
                    continue;
                Vector3 newPosition = offest * i * scale;
                if (ancor == Ancor.right)
                    newPosition -= offest * scale * (text.Length - 1);
                else if (ancor == Ancor.right)
                    newPosition -= offest * scale * (text.Length - 1) / 2;
                letter = GetLetter(text[i]);
                if (letter)
                {
                    newLetter = GameObject.Instantiate(letter) as GameObject;
                    newLetter.transform.SetParent(parent);
                    newLetter.transform.localPosition = newPosition;
                    newLetter.transform.localScale *= scale;

                    ApplyColor(newLetter);
                    children.Add(newLetter);
                }
            }
        }

        /// <summary>
        /// Sets the material of all children of the __obj__ to _material
        /// </summary>
        /// <param name="obj"></param>
        void ApplyColor(GameObject obj)
        {
            Renderer[] meshRenderers = obj.GetComponentsInChildren<Renderer>() as Renderer[];
            foreach (Renderer item in meshRenderers)
            {
                item.material = _material;
            }
        }

        /// <summary>
        /// Sets the local variable text to __text__
        /// </summary>
        /// <param name="text">the text we are going to change to</param>
        public void SetText(string text)
        {
            this.text = text;
        }

        /// <summary>
        /// Retuns a GameObject with the 3D model of the __letter__ parameter.
        /// </summary>
        /// <param name="letter">The letter we want to get the 3D model of</param>
        /// <returns>The GameObject reference to the appropriate letter</returns>
        GameObject GetLetter(char letter)
        {
            foreach (var item in letters)
            {
                if (item.name == letter.ToString())
                    return item;
            }
            return null;
        }
    }
}
