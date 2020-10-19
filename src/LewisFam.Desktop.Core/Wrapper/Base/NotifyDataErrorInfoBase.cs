// ***********************************************************************
// Assembly         : EnhancedExport
// Author           : Terrell.Lewis@verint.com
// ***********************************************************************
// <copyright file="NotifyDataErrorInfoBase.cs" company="Verint Systems Inc.">
//     Copyright © Verint Systems Inc. 2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using LewisFam.Desktop.Core.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace LewisFam.Desktop.Core.Wrapper
{
    /// <summary>
    /// Class NotifyDataErrorInfoBase.
    /// Implements the <see cref="ExportViewModelBase" />
    /// Implements the <see cref="INotifyDataErrorInfo" />
    /// </summary>
    /// <seealso cref="ExportViewModelBase" />
    /// <seealso cref="INotifyDataErrorInfo" />
    public class NotifyDataErrorInfoBase : ViewModelBase, INotifyDataErrorInfo
    {
        /// <summary>
        /// The errors
        /// </summary>
        protected readonly Dictionary<string, List<string>> Errors;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyDataErrorInfoBase" /> class.
        /// </summary>
        protected NotifyDataErrorInfoBase()
        {
            Errors = new Dictionary<string, List<string>>();
        }

        /// <summary>
        /// Gets a value that indicates whether the entity has validation errors.
        /// </summary>
        /// <value><c>true</c> if this instance has errors; otherwise, <c>false</c>.</value>
        public bool HasErrors => Errors.Any();

        /// <summary>
        /// Occurs when the validation errors have changed for a property or for the entire entity.
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// Gets the validation errors for a specified property or for the entire entity.
        /// </summary>
        /// <param name="propertyName">The name of the property to retrieve validation errors for; or null or <see cref="F:System.String.Empty" />, to retrieve entity-level errors.</param>
        /// <returns>The validation errors for the property or entity.</returns>
        public IEnumerable GetErrors(string propertyName)
        {
            return propertyName != null && Errors.ContainsKey(propertyName)
              ? Errors[propertyName]
              : Enumerable.Empty<string>();
        }

        /// <summary>
        /// Called when [errors changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Clears the errors.
        /// </summary>
        protected void ClearErrors()
        {
            foreach (var propertyName in Errors.Keys.ToList())
            {
                Errors.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }
    }
}