using System.Security.Cryptography;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Controls.Primitives;
using Avalonia.Input.Platform;

namespace PasswordGenerator;

public partial class MainWindow : Window
{
    private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
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
        var characters = new char[length];
        for (var i = 0; i < characters.Length; i++)
            characters[i] = Alphabet[RandomNumberGenerator.GetInt32(Alphabet.Length)];

        _currentPassword = new string(characters);
        PasswordText.Text = _currentPassword;
        StatusText.Text = "";
    }
}
