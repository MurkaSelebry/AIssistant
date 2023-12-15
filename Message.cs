// User.cs
// This is a personal academic project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

public class User
{
    public string Username { get; }
    public string Password { get; }
    public string Name { get; set; }
    public List<User> Friends { get; } // Список друзей
    public List<Message> Messages { get; } // Список сообщений

    public User(string username, string password, string name)
    {
        Username = username;
        Password = password;
        Name = name;
        Friends = new List<User>();
        Messages = new List<Message>();
    }

    public void AddFriend(User friend)
    {
        if (!Friends.Contains(friend))
        {
            Friends.Add(friend);
        }
    }

    public void SendMessage(User friend, string text)
    {
        if (Friends.Contains(friend))
        {
            Message message = new Message(text, DateTime.Now, this);
            friend.Messages.Add(message);
            Messages.Add(message);
        }
        else
        {
            Messages.Add(new Message(null,DateTime.Now,null));
        }

    }

}

// Message.cs
public class Bot: User
{
    public ObservableCollection<Message> AnwerMessages { get; } // Список сообщений

    public Bot(string username, string password, string name) : base(username, password, name) {
      //  AnwerMessages.CollectionChanged += Messages_CollectionChanged;
    }

    private void Messages_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (Messages.Count > 0)
        {
            var lastMessage = Messages[Messages.Count - 1];
            if (lastMessage.Text != null)
            {
                SendMessage(lastMessage.Sender, "Да, это действительно чат бот");
            }
        }
    }

    public async Task<string> AnswerMessage(string messageText)
    {
        var lastMessage = Messages[Messages.Count - 1];

        string fileName = $"main.py {lastMessage};{messageText}";

        Process p = new Process();
        p.StartInfo = new ProcessStartInfo("py", fileName)
        {
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        StringBuilder outputBuilder = new StringBuilder();

        p.OutputDataReceived += (sender, args) =>
        {
            if (!string.IsNullOrEmpty(args.Data))
            {
                outputBuilder.AppendLine(args.Data);
            }
        };

        p.Start();
        p.BeginOutputReadLine(); // Асинхронное чтение вывода

        await p.WaitForExitAsync().ConfigureAwait(false);

        return outputBuilder.ToString();
    }

}


public class Message
{
    public string Text { get; }
    public DateTime Date { get; }
    public User Sender { get; }


    public Message(string text, DateTime date, User sender)
    {
        Text = text;
        Date = date;
        Sender = sender;
// alignment = sender == currentUser ? HorizontalAlignment.Right : HorizontalAlignment.Left;
    }

    public System.Windows.HorizontalAlignment Alignment { get; set; }

}
