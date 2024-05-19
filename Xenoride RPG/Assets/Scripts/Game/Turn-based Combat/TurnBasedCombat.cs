using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;


namespace Xenoride.TBC
{

    public class TurnBasedCombat : GameModule
    {


        [SerializeField] internal Transform sceneSpace;
        [SerializeField] private TBC_MainUI mainUI;
        [SerializeField] private TBC_TurnSystem turnSystem;
        [SerializeField] private TBC_CameraController cameraController;

        public static TBC_MainUI UI
        {
            get { return _instance.mainUI; }
        }
        public static TBC_TurnSystem Turn
        {
            get { return _instance.turnSystem; }
        }
        public static TBC_CameraController Camera
        {
            get { return _instance.cameraController; }
        }

        [Space]
        [Header("Game")]


        private static TurnBasedCombat _instance;
        public static TurnBasedCombat Instance
        {
            get
            {
                if (_instance == null) return FindObjectOfType<TurnBasedCombat>();
                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this;
            Engine.AddGameModule(this);
        }

        private void Start()
        {
            //initate load save file from XenoSave.
        }


    }
}