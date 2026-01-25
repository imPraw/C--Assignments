bool exit = false; 

List<TaskItem> tasks = new List<TaskItem> ();

while (!exit)
{
Console.WriteLine ("======== Task Management System ==========="); 
Console.WriteLine ("==========================================="); 
Console.WriteLine ("1. Add task: "); 
Console.WriteLine ("2. View all tasks: "); 
Console.WriteLine ("3. Mark task as completed: "); 
Console.WriteLine ("4. Delete task: "); 
Console.WriteLine ("5. Filter Tasks by Priority: "); 
Console.WriteLine ("6. Sort Tasks by Due Date: "); 
Console.WriteLine ("7. Save Task to File: "); 
Console.WriteLine ("8. Exit: "); 
Console.WriteLine ("==========================================="); 
Console.Write ("Enter an option: "); 

string choice = Console.ReadLine();

switch (choice)
{
    case "1": 
        Console.Write("Enter task title: ");
        string title = Console.ReadLine();
        Console.Write("Enter task description: ");
        string description = Console.ReadLine();

        TaskItem taskItem = new TaskItem();

        taskItem.Title = title; 
        taskItem.Description = description;

        tasks.Add(taskItem);
        break;
    case "2": 
        TaskItem.ViewAllTasks();
        break;
    case "3": 
        TaskItem.MarkTaskCompleted();
        break;
    case "4": 
        TaskItem.DeleteTask();
        break;
    case "5": 
        TaskItem.FilterTaskByPriority();
        break;
    case "6": 
        TaskItem.SortTasksByDueDate();
        break;
    case "7": 
        TaskItem.SaveTaskToFile();
        break;
    case "8": 
        exit = true;
        break;
    default:
        Console.WriteLine("Invalid option");
        break;
}
}



Console.WriteLine ("Enter task: "); 
string? task = Console.ReadLine ();
Console.WriteLine($"Task is {task}" , task);




enum Priority
{
    Low,
    Medium,
    High
}

class TaskItem
{
    public string? Title {get;set;} 
    public string? Description{get;set;}
    public Priority Priority {get;set;}
    public DateTime DueDate{get;set;}
    public bool IsCompleted{get;set;}

    public static void ViewAllTasks() { }
    public static void MarkTaskCompleted() { }
    public static void DeleteTask() { }
    public static void FilterTaskByPriority() { }
    public static void SortTasksByDueDate() { }
    public static void SaveTaskToFile() { }
}