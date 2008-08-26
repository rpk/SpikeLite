/**
 * SpikeLite C# IRC Bot
 * Copyright (c) 2008 FreeNode ##Csharp Community
 * 
 * This source is licensed under the terms of the MIT license. Please see the 
 * distributed license.txt for details.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using SpikeLite.AccessControl;
using SpikeLite.Communications;
using SpikeLite.Persistence;

namespace SpikeLite.Modules
{
    public class ModuleManager
    {
        #region Fields

        private AuthenticationModule _authenticationModule;
        protected CommunicationManager _communicationManager;
        private PersistenceLayer _persistenceLayer;

        private Dictionary<string, ModuleBase> _modulesByName = new Dictionary<string, ModuleBase>();
        private Dictionary<string, ModuleAttribute> _moduleAttributesByName = new Dictionary<string, ModuleAttribute>();
        
        #endregion

        public Dictionary<string, ModuleBase> ModulesByName
        {
            get { return _modulesByName; }
        }
        
        public Dictionary<string, ModuleAttribute> ModuleAttributesByName
        {
            get { return _moduleAttributesByName; }
        }

        public ModuleManager(CommunicationManager communicationManager, AuthenticationModule authenticationModule, PersistenceLayer persistenceLayer)
        {
            _communicationManager = communicationManager;
            _authenticationModule = authenticationModule;
            _persistenceLayer = persistenceLayer;

            LoadModules();

            _communicationManager.RequestReceived += new EventHandler<RequestReceivedEventArgs>(communicationManager_RequestReceived);
        }

        #region Methods

        #region LoadModules

        private void LoadModules()
        {
            //TODO AJ: This needs changing to use Spring.Net and IoC. For now we're just manually loading the modules.

            //AJ: All modules must currently reside in the root directory of the app.
            string directory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            //AJ: Loading all files in the directory is very inefficient, this could be improved by giving the modules a unique
            //AJ: file extension and only loading those files. But there's not much point since this code will be scrapped anyway.
            foreach (string file in Directory.GetFiles(directory))
            {
                try
                {
                    Assembly assembly = Assembly.LoadFrom(file);

                    foreach (Type loadedType in assembly.GetTypes())
                    {
                        if (!loadedType.IsAbstract && loadedType.IsSubclassOf(typeof(ModuleBase)))
                        {
                            ModuleAttribute[] moduleAttributes = (ModuleAttribute[])loadedType.GetCustomAttributes(typeof(ModuleAttribute), false);

                            if (moduleAttributes.Length == 1)
                            {
                                ModuleAttribute moduleAttribute = moduleAttributes[0];
                                ModuleBase module = (ModuleBase)Activator.CreateInstance(loadedType);

                                module.InitModule(this, _persistenceLayer);

                                _modulesByName.Add(moduleAttribute.Name.ToLower(), module);
                                _moduleAttributesByName.Add(moduleAttribute.Name.ToLower(), moduleAttribute);

                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine("{0} {1} Loaded Module: {2}", DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString(), moduleAttribute.Name);
                            }
                        }
                    }
                }
                //AJ: Swallowing exceptions is bad mmmmkay <insert usual excuse about temp code here>
                catch { }
            }
        }

        #endregion

        public void HandleRequest(Request request)
        {
            //TODO AJ:Add Chain of Responsability for command modules to allow for better message handling
            //TODO AJ:Once CoR has been added the include a dictionary of the first level of modules to make lookup faster
            //TODO AJ:and stop messages being passed to modules that arn't interested.

            //AJ: Forward the Request to each Module
            foreach (ModuleBase module in _modulesByName.Values)
            {
                module.HandleRequest(request);
            }
        }

        public void HandleResponse(Response response)
        {
            //AJ: Forward the Response to the Communications Manager 
            _communicationManager.SendResponse(response);
        }

        void communicationManager_RequestReceived(object sender, RequestReceivedEventArgs e)
        {
            HandleRequest(e.Request);
        }

        #endregion
    }
}