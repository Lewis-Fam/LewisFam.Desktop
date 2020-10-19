using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace LewisFam.Desktop.Core.Mvvm
{
    /// <remarks>Internal use only.</remarks>
    public abstract class Module : Prism.Modularity.IModule, IModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Module"/> class.
        /// </summary>
        protected Module()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Module"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        protected Module(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        protected virtual IRegionManager _regionManager { get; set; }

        /// <summary>On initialized.</summary>
        /// <param name="containerProvider">The container provider.</param>
        public virtual void OnInitialized(IContainerProvider containerProvider) { ContainerProvider = containerProvider; }

        /// <summary>Registers the types.</summary>
        /// <param name="containerRegistry">The container registry.</param>
        public virtual void RegisterTypes(IContainerRegistry containerRegistry) { }

        /// <summary>Gets or sets the container provider.</summary>
        protected virtual IContainerProvider ContainerProvider { get; set; }

        /// <summary>Gets the module manager.</summary>
        protected virtual IModuleManager ModuleManager => ContainerProvider.Resolve<IModuleManager>();              
    }
}