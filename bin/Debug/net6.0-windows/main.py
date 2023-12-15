import g4f
from sys import argv

def responseGpt(messages):
    response = g4f.ChatCompletion.create(
        model=g4f.models.gpt_35_turbo,
        provider=g4f.Provider.ChatBase,
        messages=messages,
    )
    print(response)
    return response

messages=[]

s = " ".join(argv[1::]).split(';')
messages.append({"role": "assistant", "content": s[0]})
messages.append({"role": "user", "content": s[1]})
response = responseGpt(messages=messages)
messages.append({"role": "assistant", "content": response})