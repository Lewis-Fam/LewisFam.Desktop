using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Regions;
using System;

namespace LewisFam.Desktop.Core.Mvvm
{
    public abstract class RegionViewModelBase : ViewModelBase, INavigationAware, IConfirmNavigationRequest
    {
        private string _title = "Prism Unity Application";

        protected IRegionManager _regionManager { get; private set; }

        protected virtual void RaiseConfirmation()
        {
            ConfirmationRequest.Raise(new Confirmation { Title = "Confirmation", Content = "Confirmation Message" }, r => Title = r.Confirmed ? "Confirmed" : "Not Confirmed");
        }

        protected virtual void RaiseCustomPopup()
        {
            CustomPopupRequest.Raise(new Notification { Title = "Custom Popup", Content = "Custom Popup Message " }, r => Title = "Good to go");
        }

        protected virtual void RaiseNotification()
        {
            NotificationRequest.Raise(new Notification { Content = "Notification Message", Title = "Notification" }, r => Title = "Notified");
        }

        public RegionViewModelBase(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public virtual DelegateCommand ConfirmationCommand => new DelegateCommand(RaiseConfirmation);

        public virtual InteractionRequest<IConfirmation> ConfirmationRequest { get; } = new InteractionRequest<IConfirmation>();

        public virtual DelegateCommand CustomPopupCommand => new DelegateCommand(RaiseCustomPopup);

        public virtual InteractionRequest<INotification> CustomPopupRequest { get; } = new InteractionRequest<INotification>();

        public virtual DelegateCommand NotificationCommand => new DelegateCommand(RaiseNotification);

        public virtual InteractionRequest<INotification> NotificationRequest { get; } = new InteractionRequest<INotification>();

        public virtual string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public virtual void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            continuationCallback(true);
        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
        }
    }
}