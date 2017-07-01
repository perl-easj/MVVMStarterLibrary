﻿using System;
using System.Collections.Generic;
using ViewState.Interfaces;

namespace ViewState.Implementation
{
    /// <summary>
    /// Implementation of IViewStateService interface. A ViewStateService
    /// is created with a set of valid view states, against which any
    /// subsequent changes in view state are validated.
    /// </summary>
    public class ViewStateService : IViewStateService
    {
        #region Instance fields
        private string _viewState;
        private List<string> _validViewStates;
        public event Action<string> ViewStateChanged;
        #endregion

        #region Constructor
        public ViewStateService(List<string> validViewStates)
        {
            _validViewStates = validViewStates;
        }
        #endregion

        #region Properties
        /// <summary>
        /// View state of object. If an attempt is made to set the view state
        /// to an unknown value, an exception is thrown. Subscribers are notified
        /// if the view state changes.
        /// </summary>
        public string ViewState
        {
            get { return _viewState; }
            set
            {
                if (!_validViewStates.Contains(value))
                {
                    throw new ArgumentException(nameof(ViewState));
                }

                _viewState = value;
                ViewStateChanged?.Invoke(_viewState);
            }
        } 
        #endregion
    }
}