using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace ShmupPlus
{

    public class Enemy : MonoBehaviour
    {

        [Header("Set in Inspector: Enemy")]
        public float speed = 10f; // The speed in m/s
        public float fireRate = 0.3f; // Seconds/shot (Unused)
        public float health = 10;
        public int score = 100; // Points earned for destroying this
        public float showDamageDuration = 0.1f; // # seconds to show damage
        public float powerUpDropChance = 1f; // Chance to drop a power-up

        [Header("Set Dynamically: Enemy")]
        public Color[] originalColors;
        public Material[] materials;// All the Materials of this & its children
        public bool showingDamage = false;
        public float damageDoneTime; // Time to stop showing damage
        public bool notifiedOfDestruction = false; // Will be used later
        public Text scoreGT;

        protected BoundsCheck bndCheck;

        private void Awake()
        {
            bndCheck = GetComponent<BoundsCheck>();
            // Get materials and colors for this GameObject and its children
            materials = Utils.GetAllMaterials(gameObject);
            originalColors = new Color[materials.Length];
            for (int i = 0; i < materials.Length; i++)
            {
                originalColors[i] = materials[i].color;
            }
        }

        private void Start()
        {
            // Find a reference to the ScoreCounter GameObject

            GameObject scoreGO = GameObject.Find("ScoreCounter");

            // Get the Text Component of that GameObject

            scoreGT = scoreGO.GetComponent<Text>();

            // Set the starting number of points to 0

            //scoreGT.text = "0";
        }

        // This is a property: A method that acts like a field
        public Vector3 pos
        {
            get
            {
                return (this.transform.position);
            }
            set
            {
                this.transform.position = value;
            }
        }

        void Update()
        {
            Move();

            if (showingDamage && Time.time > damageDoneTime)
            {
                UnShowDamage();
            }

            if (bndCheck != null && bndCheck.offDown)
            {
                // We're off the bottom, so destroy this GameObject
                Destroy(gameObject);
            }
        }

        public virtual void Move()
        {
            Vector3 tempPos = pos;
            tempPos.y -= speed * Time.deltaTime;
            pos = tempPos;
        }

        void OnCollisionEnter(Collision coll)
        {                                

            GameObject otherGO = coll.gameObject;

            switch (otherGO.tag)
            {

                case "ProjectileHero":                                           

                    Projectile p = otherGO.GetComponent<Projectile>();


                    // If this Enemy is off screen, don't damage it.

                    if (!bndCheck.isOnScreen)
                    {                                

                        Destroy(otherGO);

                        break;

                    }



                    // Hurt this Enemy

                    ShowDamage();

                    // Get the damage amount from the Main WEAP_DICT.

                    health -= Main.GetWeaponDefinition(p.type).damageOnHit;

                    if (health <= 0)
                    {


                        // Tell the Main singleton that this ship was destroyed 
                        if (!notifiedOfDestruction)
                        {


                            Main.S.shipDestroyed(this);


                        }
                        notifiedOfDestruction = true;
                        // Destroy this Enemy

                        int score = int.Parse(scoreGT.text);

                        // Add points for catching the apple

                        score += 100;

                        // Convert the score back to a string and display it

                        scoreGT.text = score.ToString();

                        if (score > HighScore.score)
                        {

                            HighScore.score = score;

                        }

                        Destroy(this.gameObject);

                    }

                    Destroy(otherGO);                                          

                    break;



                default:

                    print("Enemy hit by non-ProjectileHero: " + otherGO.name); 

                    break;



            }

        }

        void ShowDamage()
        {
            foreach (Material m in materials)
            {
                m.color = Color.red;
            }
            showingDamage = true;
            damageDoneTime = Time.time + showDamageDuration;
        }

        void UnShowDamage()
        {
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i].color = originalColors[i];
            }
            showingDamage = false;
        }
    }
}