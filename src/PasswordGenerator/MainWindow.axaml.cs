using System.Security.Cryptography;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Controls.Primitives;
using Avalonia.Input.Platform;

namespace PasswordGenerator;

public partial class MainWindow : Window
{
    private const string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Lowercase = "abcdefghijklmnopqrstuvwxyz";
    private const string Numbers = "0123456789";
    private const string Symbols = "!@#$%^&*()-_=+[]{};:,.?/";
    private const string AmbiguousCharacters = "0Ool1I";
    private string _currentPassword = "";

    public MainWindow()
    {
        InitializeComponent();
        GeneratePassword();
    }

    private void OnLengthChanged(object? sender, RangeBaseValueChangedEventArgs e)
    {
        LengthValue.Text = ((int)e.NewValue).ToString();
        GeneratePassword();
    }

    private void OnOptionsChanged(object? sender, RoutedEventArgs e) => GeneratePassword();

    private void OnGenerateClicked(object? sender, RoutedEventArgs e) => GeneratePassword();

    private async void OnCopyClicked(object? sender, RoutedEventArgs e)
    {
        var clipboard = TopLevel.GetTopLevel(this)?.Clipboard;
        if (clipboard is null)
        {
            StatusText.Text = "Clipboard is unavailable.";
            return;
        }

        try
        {
            await clipboard.SetTextAsync(_currentPassword);
            StatusText.Text = "Copied to clipboard.";
        }
        catch (Exception)
        {
            StatusText.Text = "Could not access the clipboard.";
        }
    }

    private void GeneratePassword()
    {
        var length = Math.Clamp((int)LengthSlider.Value, 1, 100);
        var groups = new List<string>();
        if (UppercaseCheckBox.IsChecked == true) groups.Add(Uppercase);
        if (LowercaseCheckBox.IsChecked == true) groups.Add(Lowercase);
        if (NumbersCheckBox.IsChecked == true) groups.Add(Numbers);
        if (SymbolsCheckBox.IsChecked == true) groups.Add(Symbols);

        if (groups.Count == 0)
        {
            _currentPassword = "";
            PasswordText.Text = "Select at least one character group";
            StatusText.Text = "Choose uppercase, lowercase, numbers, or symbols.";
            return;
        }

        if (length < groups.Count)
        {
            StatusText.Text = $"Length must be at least {groups.Count} for the selected groups.";
            return;
        }

        if (ExcludeAmbiguousCheckBox.IsChecked == true)
            groups = groups.Select(group => new string(group.Where(c => !AmbiguousCharacters.Contains(c)).ToArray())).ToList();

        var alphabet = string.Concat(groups);
        var characters = new char[length];
        var index = 0;
        foreach (var group in groups)
            characters[index++] = group[RandomNumberGenerator.GetInt32(group.Length)];

        while (index < characters.Length)
            characters[index++] = alphabet[RandomNumberGenerator.GetInt32(alphabet.Length)];

        // Shuffle with cryptographically secure indices so the required characters
        // don't stay predictably at the beginning of the generated password.
        for (var i = characters.Length - 1; i > 0; i--)
        {
            var j = RandomNumberGenerator.GetInt32(i + 1);
            (characters[i], characters[j]) = (characters[j], characters[i]);
        }

        _currentPassword = new string(characters);
        PasswordText.Text = _currentPassword;
        StatusText.Text = "";
    }
}
