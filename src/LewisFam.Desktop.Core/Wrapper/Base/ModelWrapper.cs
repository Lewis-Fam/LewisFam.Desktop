using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace LewisFam.Desktop.Core.Wrapper
{
    /// <summary>
    /// Class ModelWrapper.
    /// Implements the <see cref="NotifyDataErrorInfoBase" />
    /// Implements the <see cref="IValidatableTrackingObject" />
    /// Implements the <see cref="IValidatableObject" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="NotifyDataErrorInfoBase" />
    /// <seealso cref="IValidatableTrackingObject" />
    /// <seealso cref="IValidatableObject" />
    public class ModelWrapper<T> : NotifyDataErrorInfoBase,
        IValidatableTrackingObject, IValidatableObject
    {
        /// <summary>
        /// The original values
        /// </summary>
        private readonly Dictionary<string, object> _originalValues;

        /// <summary>
        /// The tracking objects
        /// </summary>
        private readonly List<IValidatableTrackingObject> _trackingObjects;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelWrapper{T}" /> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <exception cref="ArgumentNullException">model</exception>
        protected ModelWrapper(T model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            Model = model;
            _originalValues = new Dictionary<string, object>();
            _trackingObjects = new List<IValidatableTrackingObject>();
            InitializeComplexProperties(model);
            InitializeCollectionProperties(model);
            Validate();
        }

        /// <summary>
        /// Initializes the complex properties.
        /// </summary>
        /// <param name="model">The model.</param>
        protected virtual void InitializeComplexProperties(T model)
        {
        }

        /// <summary>
        /// Initializes the collection properties.
        /// </summary>
        /// <param name="model">The model.</param>
        protected virtual void InitializeCollectionProperties(T model)
        {
        }

        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <value>The model.</value>
        public T Model { get; private set; }

        /// <summary>
        /// Gets the object's changed status.
        /// </summary>
        /// <value><c>true</c> if this instance is changed; otherwise, <c>false</c>.</value>
        public bool IsChanged => _originalValues.Count > 0 || _trackingObjects.Any(t => t.IsChanged);

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        public bool IsValid => !HasErrors && _trackingObjects.All(t => t.IsValid);

        /// <summary>
        /// Resets the object’s state to unchanged by accepting the modifications.
        /// </summary>
        public void AcceptChanges()
        {
            _originalValues.Clear();
            foreach (var trackingObject in _trackingObjects)
            {
                trackingObject.AcceptChanges();
            }
            RaisePropertyChanged();
        }

        /// <summary>
        /// Resets the object’s state to unchanged by rejecting the modifications.
        /// </summary>
        public void RejectChanges()
        {
            foreach (var originalValueEntry in _originalValues)
            {
                typeof(T).GetProperty(originalValueEntry.Key)?.SetValue(Model, originalValueEntry.Value);
            }

            _originalValues.Clear();
            foreach (var trackingObject in _trackingObjects)
            {
                trackingObject.RejectChanges();
            }

            Validate();
            RaisePropertyChanged();
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <typeparam name="TValue">The type of the t value.</typeparam>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>TValue.</returns>
        protected TValue GetValue<TValue>([CallerMemberName] string propertyName = null)
        {
            var propertyInfo = Model.GetType().GetProperty(propertyName);
            return (TValue)propertyInfo.GetValue(Model);
        }

        /// <summary>
        /// Gets the original value.
        /// </summary>
        /// <typeparam name="TValue">The type of the t value.</typeparam>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>TValue.</returns>
        protected TValue GetOriginalValue<TValue>(string propertyName)
        {
            return _originalValues.ContainsKey(propertyName)
                ? (TValue)_originalValues[propertyName]
                : GetValue<TValue>(propertyName);
        }

        /// <summary>
        /// Gets the is changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected bool GetIsChanged(string propertyName)
        {
            return _originalValues.ContainsKey(propertyName);
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <typeparam name="TValue">The type of the t value.</typeparam>
        /// <param name="newValue">The new value.</param>
        /// <param name="propertyName">Name of the property.</param>
        protected void SetValue<TValue>(TValue newValue,
            [CallerMemberName] string propertyName = null)
        {
            var propertyInfo = Model.GetType().GetProperty(propertyName);
            var currentValue = propertyInfo?.GetValue(Model);
            if (!Equals(currentValue, newValue))
            {
                UpdateOriginalValue(currentValue, newValue, propertyName);
                propertyInfo?.SetValue(Model, newValue);
                Validate();
                RaisePropertyChanged(propertyName);
            }
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        private void Validate()
        {
            ClearErrors();

            var results = new List<ValidationResult>();
            var context = new ValidationContext(this);
            Validator.TryValidateObject(this, context, results, true);

            if (results.Any())
            {
                var propertyNames = results.SelectMany(r => r.MemberNames).Distinct().ToList();

                foreach (var propertyName in propertyNames)
                {
                    Errors[propertyName] = results
                        .Where(r => r.MemberNames.Contains(propertyName))
                        .Select(r => r.ErrorMessage)
                        .Distinct()
                        .ToList();
                    OnErrorsChanged(propertyName);
                }
            }

            RaisePropertyChanged(nameof(IsValid));
        }

        /// <summary>
        /// Updates the original value.
        /// </summary>
        /// <param name="currentValue">The current value.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="propertyName">Name of the property.</param>
        private void UpdateOriginalValue(object currentValue, object newValue, string propertyName)
        {
            if (!_originalValues.ContainsKey(propertyName))
            {
                _originalValues.Add(propertyName, currentValue);
                RaisePropertyChanged($"IsChanged");
            }
            else
            {
                if (Equals(_originalValues[propertyName], newValue))
                {
                    _originalValues.Remove(propertyName);
                    RaisePropertyChanged($"IsChanged");
                }
            }
        }

        /// <summary>
        /// Registers the collection.
        /// </summary>
        /// <typeparam name="TWrapper">The type of the t wrapper.</typeparam>
        /// <typeparam name="TModel">The type of the t model.</typeparam>
        /// <param name="wrapperCollection">The wrapper collection.</param>
        /// <param name="modelCollection">The model collection.</param>
        protected void RegisterCollection<TWrapper, TModel>(
            ChangeTrackingCollection<TWrapper> wrapperCollection,
            List<TModel> modelCollection) where TWrapper : ModelWrapper<TModel>
        {
            wrapperCollection.CollectionChanged += (s, e) =>
            {
                modelCollection.Clear();
                modelCollection.AddRange(wrapperCollection.Select(w => w.Model));
                Validate();
            };
            RegisterTrackingObject(wrapperCollection);
        }

        /// <summary>
        /// Registers the collection.
        /// </summary>
        /// <typeparam name="TWrapper">The type of the t wrapper.</typeparam>
        /// <typeparam name="TModel">The type of the t model.</typeparam>
        /// <param name="wrapperCollection">The wrapper collection.</param>
        /// <param name="modelCollection">The model collection.</param>
        protected void RegisterCollection<TWrapper, TModel>(
            ChangeTrackingCollection<TWrapper> wrapperCollection,
            ObservableCollection<TModel> modelCollection) where TWrapper : ModelWrapper<TModel>
        {
            wrapperCollection.CollectionChanged += (s, e) =>
            {
                modelCollection.Clear();
                modelCollection.Add(wrapperCollection.Select(w => w.Model).Single());
                Validate();
            };
            RegisterTrackingObject(wrapperCollection);
        }

        /// <summary>
        /// Registers the complex.
        /// </summary>
        /// <typeparam name="TModel">The type of the t model.</typeparam>
        /// <param name="wrapper">The wrapper.</param>
        protected void RegisterComplex<TModel>(ModelWrapper<TModel> wrapper)
        {
            RegisterTrackingObject(wrapper);
        }

        /// <summary>
        /// Registers the tracking object.
        /// </summary>
        /// <param name="trackingObject">The tracking object.</param>
        private void RegisterTrackingObject(IValidatableTrackingObject trackingObject)
        {
            if (!_trackingObjects.Contains(trackingObject))
            {
                _trackingObjects.Add(trackingObject);
                trackingObject.PropertyChanged += TrackingObjectPropertyChanged;
            }
        }

        /// <summary>
        /// Trackings the object property changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs" /> instance containing the event data.</param>
        private void TrackingObjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IsChanged))
            {
                RaisePropertyChanged(nameof(IsChanged));
            }
            else if (e.PropertyName == nameof(IsValid))
            {
                RaisePropertyChanged(nameof(IsValid));
            }
        }

        /// <summary>
        /// Validates the specified validation context.
        /// </summary>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>IEnumerable&lt;ValidationResult&gt;.</returns>
        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}