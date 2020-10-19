// ***********************************************************************
// Assembly         : EnhancedExport
// Author           : Terrell.Lewis@verint.com
// ***********************************************************************
// <copyright file="IValidatableTrackingObject.cs" company="Verint Systems Inc.">
//     Copyright © Verint Systems Inc. 2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel;

namespace LewisFam.Desktop.Core.Wrapper
{
    /// <summary>
    /// Interface IValidatableTrackingObject
    /// Implements the <see cref="IRevertibleChangeTracking" />
    /// Implements the <see cref="INotifyPropertyChanged" />
    /// </summary>
    /// <seealso cref="IRevertibleChangeTracking" />
    /// <seealso cref="INotifyPropertyChanged" />
    public interface IValidatableTrackingObject :
    IRevertibleChangeTracking,
    INotifyPropertyChanged
    {
        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        bool IsValid { get; }
    }
}