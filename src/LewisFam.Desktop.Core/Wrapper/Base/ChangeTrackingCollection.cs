using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace LewisFam.Desktop.Core.Wrapper
{
    /// <summary>
    /// Class ChangeTrackingCollection.
    /// Implements the <see cref="System.Collections.ObjectModel.ObservableCollection{T}" />
    /// Implements the <see cref="IValidatableTrackingObject" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Collections.ObjectModel.ObservableCollection{T}" />
    /// <seealso cref="IValidatableTrackingObject" />
    public class ChangeTrackingCollection<T> : ObservableCollection<T>, IValidatableTrackingObject
        where T : class, IValidatableTrackingObject
    {
        /// <summary>
        /// The original collection
        /// </summary>
        private IList<T> _originalCollection;

        /// <summary>
        /// The added items
        /// </summary>
        private ObservableCollection<T> _addedItems;

        /// <summary>
        /// The removed items
        /// </summary>
        private ObservableCollection<T> _removedItems;

        /// <summary>
        /// The modified items
        /// </summary>
        private ObservableCollection<T> _modifiedItems;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeTrackingCollection{T}" /> class.
        /// </summary>
        /// <param name="items">The items.</param>
        public ChangeTrackingCollection(IEnumerable<T> items)
            : base(items)
        {
            _originalCollection = this.ToList();

            AttachItemPropertyChangedHandler(_originalCollection);

            _addedItems = new ObservableCollection<T>();
            _removedItems = new ObservableCollection<T>();
            _modifiedItems = new ObservableCollection<T>();

            AddedItems = new ReadOnlyObservableCollection<T>(_addedItems);
            RemovedItems = new ReadOnlyObservableCollection<T>(_removedItems);
            ModifiedItems = new ReadOnlyObservableCollection<T>(_modifiedItems);
        }

        /// <summary>
        /// Gets the added items.
        /// </summary>
        /// <value>The added items.</value>
        public ReadOnlyObservableCollection<T> AddedItems { get; private set; }

        /// <summary>
        /// Gets the removed items.
        /// </summary>
        /// <value>The removed items.</value>
        public ReadOnlyObservableCollection<T> RemovedItems { get; private set; }

        /// <summary>
        /// Gets the modified items.
        /// </summary>
        /// <value>The modified items.</value>
        public ReadOnlyObservableCollection<T> ModifiedItems { get; private set; }

        /// <summary>
        /// Gets the object's changed status.
        /// </summary>
        /// <value><c>true</c> if this instance is changed; otherwise, <c>false</c>.</value>
        public bool IsChanged => AddedItems.Count > 0 || RemovedItems.Count > 0 || ModifiedItems.Count > 0;

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        public bool IsValid => this.All(t => t.IsValid);

        /// <summary>
        /// Resets the object’s state to unchanged by accepting the modifications.
        /// </summary>
        public void AcceptChanges()
        {
            _addedItems.Clear();
            _modifiedItems.Clear();
            _removedItems.Clear();

            foreach (var item in this)
            {
                item.AcceptChanges();
            }

            _originalCollection = this.ToList();

            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsChanged)));
        }

        /// <summary>
        /// Resets the object’s state to unchanged by rejecting the modifications.
        /// </summary>
        public void RejectChanges()
        {
            foreach (var addedItem in _addedItems.ToList())
            {
                Remove(addedItem);
            }

            foreach (var removedItem in _removedItems.ToList())
            {
                removedItem.RejectChanges(); // catches changes that were first modified and then removed
                Add(removedItem);
            }

            foreach (var modifiedItem in _modifiedItems.ToList())
            {
                modifiedItem.RejectChanges();
            }

            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsChanged)));
        }

        /// <summary>
        /// Handles the <see cref="E:CollectionChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="NotifyCollectionChangedEventArgs" /> instance containing the event data.</param>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            var added = this.Where(current => _originalCollection.All(orig => orig != current));
            var removed = _originalCollection.Where(orig => this.All(current => current != orig));

            var modified = this.Except(added).Except(removed).Where(item => item.IsChanged).ToList();

            AttachItemPropertyChangedHandler(added);
            DetachItemPropertyChangedHandler(removed);

            UpdateObservableCollection(_addedItems, added);
            UpdateObservableCollection(_removedItems, removed);
            UpdateObservableCollection(_modifiedItems, modified);

            base.OnCollectionChanged(e);
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsChanged)));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsValid)));
        }

        /// <summary>
        /// Items the property changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs" /> instance containing the event data.</param>
        private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IsValid))
            {
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsValid)));
            }
            else
            {
                var item = (T)sender;
                if (_addedItems.Contains(item))
                {
                    return;
                }

                if (item.IsChanged)
                {
                    if (!_modifiedItems.Contains(item))
                    {
                        _modifiedItems.Add(item);
                    }
                }
                else
                {
                    if (_modifiedItems.Contains(item))
                    {
                        _modifiedItems.Remove(item);
                    }
                }

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsChanged)));
            }
        }

        /// <summary>
        /// Attaches the item property changed handler.
        /// </summary>
        /// <param name="items">The items.</param>
        private void AttachItemPropertyChangedHandler(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                item.PropertyChanged += ItemPropertyChanged;
            }
        }

        /// <summary>
        /// Detaches the item property changed handler.
        /// </summary>
        /// <param name="items">The items.</param>
        private void DetachItemPropertyChangedHandler(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                item.PropertyChanged -= ItemPropertyChanged;
            }
        }

        /// <summary>
        /// Updates the observable collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="items">The items.</param>
        private static void UpdateObservableCollection(ICollection<T> collection, IEnumerable<T> items)
        {
            collection.Clear();
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
    }
}