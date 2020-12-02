using System.Collections.Generic;
using UnityEngine;

namespace Combat 
{
    public class DamageHighlight : MonoBehaviour
    {
        [SerializeField] private GameObject _charPrefab;
        private List<Material> _materials;
        private Material [] _cleanColors;
        private float highlightDuration = 0.2f;
        private float higlightEnd = 0.0f;
        private bool damaged = false;
        
        private void Awake()
        {
            _materials = new List<Material>();

            foreach(Renderer renderer in _charPrefab.GetComponentsInChildren<Renderer>())
            {
                foreach(Material material in renderer.materials)
                {
                    _materials.Add(material);
                }
            }
        
            Material [] materials = new Material[_materials.Count];
            _materials.CopyTo(0, materials, 0, _materials.Count);

            _cleanColors = new Material[_materials.Count];
            for (int i = 0; i < materials.Length; i++)
            {
                _cleanColors[i] = new Material(materials[i]);
            }
        }
        void Update()
        {
            if (Time.time > higlightEnd && damaged == true)
            {
                RemoveDamageMaterial();
            }
        }
        public void HighLight()
        {
            higlightEnd = Time.time + highlightDuration;
            foreach(Material material in _materials)
            {
                material.color = Color.red;
            }
            damaged = true;
        }
        private void RemoveDamageMaterial()
        {
            int i = 0;
            foreach(Material material in _materials)
            {
                material.color = _cleanColors[i].color;
                i++;
            }
        }
    }
}

