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
        public enum Ancor
        {
            left,
            middle,
            right
        }

        public Transform parent; //!< the position of the parent is the at which the last digit stands
        public Vector3 offest; //!< the offset which the digits should have between each other
        public GameObject[] letters; //!< the models of the letters

        public string text;
        public Ancor ancor;
        public float scale = 1;

        public Material material;
        public Color color;

        private string _text;
        private List<GameObject> children;
        private Material _material;

        void Start()
        {
            _material = new Material(material);
            children = new List<GameObject>();
            foreach (Transform item in parent.transform)
            {
                children.Add(item.gameObject);
            }
        }

        void Update()
        {
            _material.color = color;

            _text = text;

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

        void ApplyColor(GameObject obj)
        {
            Renderer[] meshRenderers = obj.GetComponentsInChildren<Renderer>() as Renderer[];
            foreach (Renderer item in meshRenderers)
            {
                item.material = _material;
            }
        }

        public void SetText(string text)
        {
            this.text = text;
        }

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
