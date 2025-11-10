using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Text.Json;
using System.Collections.ObjectModel;
using System.IO;
using System;


namespace TodoListApp;

public partial class MainWindow : Window
{
    private ObservableCollection<string> _tasks = new();

    // Path to save the JSON file
    private readonly string _filePath = "tasks.json";

    public MainWindow()
    {
        InitializeComponent();
        TaskList.ItemsSource = _tasks;

        AddButton.Click += OnAddClick;
        DeleteButton.Click += OnDeleteClick;
        SaveButton.Click += OnSaveClick;
        LoadButton.Click += OnLoadClick;
    }

    private void OnAddClick(object? sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(TaskInput.Text))
        {
            _tasks.Add(TaskInput.Text);
            TaskInput.Text = string.Empty;
        }
    }

    private void OnDeleteClick(object? sender, RoutedEventArgs e)
    {
        if (TaskList.SelectedItem is string selected)
        {
            _tasks.Remove(selected);
        }
    }

    private void OnSaveClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            string json = JsonSerializer.Serialize(_tasks);
            File.WriteAllText(_filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving tasks: {ex.Message}");
        }
    }

    private void OnLoadClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
                var tasksFromFile = JsonSerializer.Deserialize<ObservableCollection<string>>(json);
                if (tasksFromFile != null)
                {
                    _tasks.Clear();
                    foreach (var task in tasksFromFile)
                        _tasks.Add(task);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading tasks: {ex.Message}");
        }
    }
}
