using System;
using System.Collections.Generic;
using ViewState.Interfaces;

namespace ViewState.Implementation
{
    public class ViewStateService : IViewStateService
    {
        private string _viewState;
        private List<string> _validViewStates;
        public event Action<string> ViewStateChanged;

        public ViewStateService(List<string> validViewStates)
        {
            _validViewStates = validViewStates;
        }

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
    }
}