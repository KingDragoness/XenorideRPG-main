using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

namespace Xenoride
{

    public abstract class GameModule : MonoBehaviour
    {
        public Module module = Module.Engine;
    }

	public class Engine : GameModule
    {

		[SerializeField] private ConsoleCommand _consoleCommand;
        [SerializeField] private List<GameModule> _allLoadedGameModules = new List<GameModule>();

        public static ConsoleCommand Console
        {
            get
            {
                return Instance._consoleCommand;
            }
        }

        private static Engine _instance;

        public static Engine Instance
        {
            get
            {
                if (_instance == null) return FindObjectOfType<Engine>();
                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this;
            Application.targetFrameRate = 40;
            addGameModule(this);
        }


        public static void AddGameModule(GameModule _gameModule)
        {
            Instance.addGameModule(_gameModule);
        }

        private void addGameModule(GameModule _gameModule)
        {
            if (_allLoadedGameModules.Contains(_gameModule)) return;

            _allLoadedGameModules.RemoveAll(x => x == null);
            _allLoadedGameModules.Add(_gameModule);
        }


        public static bool CheckModuleLoaded(Module moduleType)
        {
            return Instance.checkModuleLoaded(moduleType);
        }

        private bool checkModuleLoaded(Module moduleType)
        {
            _allLoadedGameModules.RemoveAll(x => x == null);

            return (_allLoadedGameModules.FindIndex(x => x.module == moduleType) != -1) ? true : false;
        }


        public static List<Module> CheckAllModulesLoaded()
        {
            return Instance.checkAllModulesLoaded();
        }

        private List<Module> checkAllModulesLoaded()
        {
            _allLoadedGameModules.RemoveAll(xl => xl == null);

            Module[] moduleList = new Module[_allLoadedGameModules.Count];

            int x = 0;
            foreach(var moduleScript in _allLoadedGameModules)
            {
                moduleList[x] = moduleScript.module;
                x++;
            }

            return moduleList.ToList();
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.BackQuote))
            {
                _consoleCommand.gameObject.SetActive(!_consoleCommand.gameObject.activeSelf);
            }
        }

    }
}