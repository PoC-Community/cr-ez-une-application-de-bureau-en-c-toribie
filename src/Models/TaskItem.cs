namespace TodoListApp.Models;
using System.Collections.Generic;

public class TaskItem
{
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; } = false;
}
