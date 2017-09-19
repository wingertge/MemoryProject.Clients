using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MemoryClient.Cross.WPF.Annotations;

namespace MemoryClient.Cross.WPF.Components
{
    /// <inheritdoc />
    /// <summary>
    /// Interaction logic for ValidatedTextBox.xaml
    /// </summary>
    public partial class ValidatedTextBox : INotifyPropertyChanged
    {
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ValidatedTextBox), new PropertyMetadata(null));



        public string OverrideError
        {
            get => (string)GetValue(OverrideErrorProperty);
            set => SetValue(OverrideErrorProperty, value);
        }

        // Using a DependencyProperty as the backing store for OverrideError.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OverrideErrorProperty =
            DependencyProperty.Register("OverrideError", typeof(string), typeof(ValidatedTextBox), new PropertyMetadata(null));



        public string OverrideWarning
        {
            get => (string)GetValue(OverrideWarningProperty);
            set => SetValue(OverrideWarningProperty, value);
        }

        // Using a DependencyProperty as the backing store for OverrideWarning.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OverrideWarningProperty =
            DependencyProperty.Register("OverrideWarning", typeof(string), typeof(ValidatedTextBox), new PropertyMetadata(null));

        public Func<string, string> ErrorValidator
        {
            get => (Func<string, string>)GetValue(ErrorValidatorProperty);
            set => SetValue(ErrorValidatorProperty, value);
        }

        // Using a DependencyProperty as the backing store for ErrorValidator.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ErrorValidatorProperty =
            DependencyProperty.Register("ErrorValidator", typeof(Func<string, string>), typeof(ValidatedTextBox), new PropertyMetadata(default(Func<string, string>)));

        public Func<string,string> WarningValidator
        {
            get => (Func<string,string>)GetValue(WarningValidatorProperty);
            set => SetValue(WarningValidatorProperty, value);
        }

        // Using a DependencyProperty as the backing store for WarningValidator.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WarningValidatorProperty =
            DependencyProperty.Register("WarningValidator", typeof(Func<string,string>), typeof(ValidatedTextBox), new PropertyMetadata(default(Func<string, string>)));

        public bool IsValid => string.IsNullOrEmpty(ErrorMessage) && string.IsNullOrEmpty(WarningMessage);

        // Using a DependencyProperty as the backing store for IsValid.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsValidProperty =
            DependencyProperty.RegisterReadOnly("IsValid", typeof(bool), typeof(ValidatedTextBox), new PropertyMetadata(true)).DependencyProperty;

        private string _errorMessage;
        public string ErrorMessage
        {
            get => OverrideError ?? _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        private string _warningMessage;
        public string WarningMessage
        {
            get => OverrideWarning ?? _warningMessage;
            set
            {
                _warningMessage = value;
                OnPropertyChanged();
            }
        }

        public Visibility ErrorVisibility => string.IsNullOrEmpty(OverrideError) ? Visibility.Collapsed : Visibility.Visible;
        public Visibility WarningVisibility => string.IsNullOrEmpty(OverrideWarning) || !string.IsNullOrEmpty(OverrideError) ? Visibility.Collapsed : Visibility.Visible;

        public ValidatedTextBox()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void ErrorMouseEnter(object sender, MouseEventArgs e) => ErrorPopover.Visibility = Visibility.Visible;
        protected void ErrorMouseLeave(object sender, MouseEventArgs e) => ErrorPopover.Visibility = Visibility.Collapsed;

        protected void OnTextChanged(string text)
        {
            var error = ErrorValidator?.Invoke(text);
            if (!string.IsNullOrEmpty(error))
            {
                ErrorMessage = error;
                return;
            }

            ErrorMessage = null;
            var warning = WarningValidator?.Invoke(text);

            if (!string.IsNullOrEmpty(warning))
            {
                WarningMessage = warning;
                return;
            }

            WarningMessage = null;
        }

        private void TextChanged(object sender, TextChangedEventArgs e) => OnTextChanged(((TextBox)sender).Text);
    }
}
