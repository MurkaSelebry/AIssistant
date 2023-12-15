using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace Social_Network
{
    public class GPTConfig
    {
    }

    public class PythonInterop
    {
        private readonly ScriptEngine _engine;
        private readonly ScriptScope _scope;

        public PythonInterop()
        {
            _engine = Python.CreateEngine();
            _scope = _engine.CreateScope();
        }

        public async Task<string> ResponseGpt(List<Dictionary<string, string>> messages)
        {
            string pythonCode = @"
import g4f
import asyncio

async def responseGpt(messages):
    response = await g4f.ChatCompletion.create(
        model=g4f.models.gpt_35_turbo,
        provider=g4f.Provider.ChatgptAi,
        messages=messages,
    )
    return response

messages = []
while True:
    messages.append({'role': 'user', 'content': input()})
    response = asyncio.run(responseGpt(messages=messages))
    messages.append({'role': 'assistant', 'content': response})
";

            dynamic result = await Task.Run(() => _engine.Execute(pythonCode, _scope));

            // Преобразование результата в строку
            string pythonResult = result.ToString();

          

            return pythonResult;
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            PythonInterop pythonInterop = new PythonInterop();

            // Пример списка сообщений
            List<Dictionary<string, string>> messages = new List<Dictionary<string, string>>();

            while (true)
            {
                Console.Write("User message: ");
                string userMessage = "Hi";

                messages.Add(new Dictionary<string, string>
                {
                    {"role", "user"},
                    {"content", userMessage}
                });

                string response = await pythonInterop.ResponseGpt(messages);

                Console.WriteLine($"Assistant response: {response}");
            }
        }
    }
}
